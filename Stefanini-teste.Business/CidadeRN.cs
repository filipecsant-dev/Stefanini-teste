using Stefanini.DAO;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Business
{
    public class CidadeRN : RNBase<CidadeDTO>
    {
        public static ResponseCrudVM Create(CidadeDTO _cidade)
        {
            try
            {
                bool result = false;
                var consult = FindOne(x => x.Nome == _cidade.Nome && x.UF == _cidade.UF && x.IsDisabled == false);
                
                if(consult == null)
                {
                    using(var uow = new UOW())
                    {
                        uow.CidadeRepository.Insert(_cidade);
                        result = uow.Commit() > 0;
                    }

                    if (result)
                    {
                        return new ResponseCrudVM
                        {
                            Status = Model.Enums.StatusCrud.Sucesso,
                            Msg = "Cidade cadastrada com sucesso!",
                            Objeto = _cidade
                        };
                    }
                    else
                    {
                        return new ResponseCrudVM
                        {
                            Status = Model.Enums.StatusCrud.Sucesso,
                            Msg = "Ops! Ocorreu um erro ao cadastrar a cidade."
                        };
                    }
                    
                }
                else
                {
                    return new ResponseCrudVM
                    {
                        Status = Model.Enums.StatusCrud.Falha,
                        Msg = "Esta cidade já está cadastrada."
                        
                    };
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public static ResponseCrudVM Atualizar(CidadeDTO _cidade)
        {
            try
            {
                bool result = false;
                var consult = FindOne(x => x.Id == _cidade.Id && x.IsDisabled == false);

                if (consult != null)
                {
                    using (var uow = new UOW())
                    {
                        uow.CidadeRepository.Update(_cidade);
                        result = uow.Commit() > 0;

                        return new ResponseCrudVM
                        {
                            Status = Model.Enums.StatusCrud.Sucesso,
                            Msg = "Atualizado com sucesso!"

                        };
                    }

                }
                else
                {
                    return new ResponseCrudVM
                    {
                        Status = Model.Enums.StatusCrud.Falha,
                        Msg = "Nenhuma cidade encontrada."

                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static ResponseGetVM ReadAll()
        {
            try
            {
                using (var uow = new UOW())
                {
                    var dados = uow.CidadeRepository.GetAll(x => x.IsDisabled == false);

                    return new ResponseGetVM
                    {
                        dados = dados
                    };
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
