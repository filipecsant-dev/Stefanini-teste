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
    public class PessoaRN : RNBase<PessoaDTO>
    {

        public static ResponseCrudVM Create(PessoaDTO _pessoa)
        {
            try
            {
                bool result = false;
                var consult = FindOne(x => x.Nome == _pessoa.Nome || x.CPF == _pessoa.CPF && x.IsDisabled == false);


                if (consult == null)
                {
                    using (var uow = new UOW())
                    {
                        uow.PessoaRepository.Insert(_pessoa);
                        result = uow.Commit() > 0;
                    }

                    if (result)
                    {
                        return new ResponseCrudVM
                        {
                            Status = Model.Enums.StatusCrud.Sucesso,
                            Msg = "Pessoa cadastrada com sucesso!",
                            Objeto = _pessoa
                        };
                    }
                    else
                    {
                        return new ResponseCrudVM
                        {
                            Status = Model.Enums.StatusCrud.Sucesso,
                            Msg = "Ops! Ocorreu um erro ao cadastrar a pessoa."
                        };
                    }

                }
                else
                {
                    return new ResponseCrudVM
                    {
                        Status = Model.Enums.StatusCrud.Falha,
                        Msg = "Esta pessoa já está cadastrada."

                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static ResponseCrudVM Atualizar(PessoaDTO _pessoa)
        {
            try
            {
                bool result = false;
                var consult = FindOne(x => x.Id == _pessoa.Id && x.IsDisabled == false);

                if (consult != null)
                {
                    using (var uow = new UOW())
                    {
                        uow.PessoaRepository.Update(_pessoa);
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
                        Msg = "Nenhuma pessoa encontrada."

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
                    var dados = uow.PessoaRepository.ReadAll();

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

        public static PessoaReadVM ReadOne(int id)
        {
            try
            {
                using (var uow = new UOW())
                {
                   return uow.PessoaRepository.ReadOne(id);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
