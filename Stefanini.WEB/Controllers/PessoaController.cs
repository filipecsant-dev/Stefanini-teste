using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stefanini.Business;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ImplementionCors")]
    public class PessoaController : Controller
    {

        private readonly IMapper _mapper;

        public PessoaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<PessoaInsertVM> Inserir(PessoaInsertVM _pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cidade = CidadeRN.FindOne(x => x.UF == _pessoa.Cidade.UF && x.Nome == _pessoa.Cidade.Nome);
                    if (cidade != null)
                    {
                        PessoaDTO pessoa = _mapper.Map<PessoaDTO>(_pessoa);
                        pessoa.Id_Cidade = cidade.Id;
                        pessoa.Cidade = null;
                        var result = PessoaRN.Create(pessoa);

                        if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                        {
                            return CreatedAtAction("Pessoa", new { id = pessoa.Id }, pessoa);
                        }
                        else
                        {
                            return BadRequest(result.Msg);
                        }
                    }
                    else
                    {
                        return BadRequest("Cidade não existe!");
                    }
                }
                else
                {
                    string error = ModelState.Select(x => x.Value.Errors).Where(y => y.Count() > 0).ToString();
                    return BadRequest(error);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult<PessoaReadVM> ReadAll()
        {
            try
            {
                var result = PessoaRN.ReadAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<PessoaReadVM> ReadOne(int id)
        {
            try
            {
                var dados = PessoaRN.ReadOne(id);

                if (dados != null)
                    return Ok(new ResponseGetOneVM { dados = dados });
                else
                    return BadRequest("Pessoa não encontrada.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existe = PessoaRN.FindOne(x => x.Id == id && x.IsDisabled == false);
                if (existe != null)
                {
                    PessoaRN.Delete(id);
                    return NoContent();
                }
                else
                {
                    return BadRequest("Pessoa não existe.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, PessoaInsertVM _pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cidade = CidadeRN.FindOne(x => x.UF == _pessoa.Cidade.UF && x.Nome == _pessoa.Cidade.Nome);
                    if (cidade != null)
                    {
                        PessoaDTO pessoa = _mapper.Map<PessoaDTO>(_pessoa);
                        pessoa.Id = id;
                        pessoa.Id_Cidade = cidade.Id;
                        pessoa.Cidade = cidade;
                        var result = PessoaRN.Atualizar(pessoa);

                        if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                            return Ok(Json(result.Msg));
                        else
                            return BadRequest(result.Msg);
                    }
                    else
                    {
                        return BadRequest("Cidade não existe!");
                    }
                }
                else
                {
                    string error = ModelState.Select(x => x.Value.Errors).Where(y => y.Count() > 0).ToString();
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
