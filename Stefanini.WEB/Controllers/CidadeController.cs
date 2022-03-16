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
    public class CidadeController : Controller
    {
        private readonly IMapper _mapper;

        public CidadeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Create
        [HttpPost]
        public ActionResult<CidadeVM> Inserir(CidadeVM _cidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CidadeDTO cidade = _mapper.Map<CidadeDTO>(_cidade);
                    var result = CidadeRN.Create(cidade);

                    if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                    {
                        return CreatedAtAction("Cidade", new { id = cidade.Id }, cidade);
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
        //Read
        public ActionResult<CidadeDTO> ReadAll()
        {
            try
            {
                var result = CidadeRN.ReadAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("carregarcidades/{uf}")]
        //Read
        public ActionResult<CidadeDTO> ReadAll(string uf)
        {
            try
            {
                var result = CidadeRN.Get(x => x.UF == uf && x.IsDisabled == false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<CidadeDTO> ReadOne(int id)
        {
            try
            {
                var dados = CidadeRN.FindOne(x => x.Id == id && x.IsDisabled == false);

                if (dados != null)
                    return Ok(new ResponseGetOneVM { dados = dados });
                else
                    return BadRequest("Cidade não encontrada.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existe = CidadeRN.FindOne(x => x.Id == id && x.IsDisabled == false);
                if (existe != null)
                {
                    CidadeRN.Delete(id);
                    return NoContent();
                }
                else
                {
                    return BadRequest("Cidade não existe.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CidadeVM _cidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CidadeDTO cidade = _mapper.Map<CidadeDTO>(_cidade);
                    cidade.Id = id;
                    var result = CidadeRN.Atualizar(cidade);

                    if (result.Status == Stefanini.Model.Enums.StatusCrud.Sucesso)
                        return Ok(Json(result.Msg));
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
