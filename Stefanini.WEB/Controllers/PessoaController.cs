using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stefanini.Business;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : Controller
    {

        private readonly IMapper _mapper;

        public PessoaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Inserir(PessoaVM _pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PessoaDTO pessoa = _mapper.Map<PessoaDTO>(_pessoa);
                    var result = PessoaRN.Create(pessoa);

                    if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Msg);
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
        public IActionResult ReadAll()
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

        [HttpGet("id:int")]
        public IActionResult ReadOne(int id)
        {
            try
            {
                var result = PessoaRN.ReadOne(id);

                if (result != null)
                    return Ok(result);
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

        [HttpPut("id:int")]
        public IActionResult Update(int id, PessoaVM _pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PessoaDTO pessoa = _mapper.Map<PessoaDTO>(_pessoa);
                    pessoa.Id = id;
                    var result = PessoaRN.Atualizar(pessoa);

                    if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                        return Ok(result.Msg);
                    else
                        return BadRequest(result.Msg);
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
