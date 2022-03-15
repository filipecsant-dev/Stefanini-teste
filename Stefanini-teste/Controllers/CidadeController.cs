using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stefanini.Business;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : Controller
    {
        private readonly IMapper _mapper;

        public CidadeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Create
        [HttpPost]
        public IActionResult Inserir(CidadeVM _cidade)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    CidadeDTO cidade = _mapper.Map<CidadeDTO>(_cidade);
                    var result = CidadeRN.Create(cidade);

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
        //Read
        public IActionResult ReadAll()
        {
            try
            {
                var result = CidadeRN.ReadAll();
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("id:int")]
        public IActionResult ReadOne(int id)
        {
            try
            {
                var result = CidadeRN.FindOne(x => x.Id == id && x.IsDisabled == false);

                if (result != null)
                    return Ok(result);
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
                if(existe != null)
                {
                    CidadeRN.Delete(id);
                    return NoContent();
                }
                else
                {
                    return BadRequest("Cidade não existe.");
                }
                    
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Update
        [HttpPut("id:int")]
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
