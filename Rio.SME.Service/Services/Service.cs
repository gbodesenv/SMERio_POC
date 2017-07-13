
namespace Rio.SME.Service.Services
{
    using Rio.SME.Domain.Contracts.Data.Global;
    using Rio.SME.Domain.Exceptions;
    using Rio.SME.Domain.Validation;
    using Rio.SME.Domain.Resources;
    using NLog;
    using System;    

    public abstract class Service : BusinessValidation
    {
        protected IUnitOfWork _uow;
        protected Logger _logger = LogManager.GetCurrentClassLogger();


        public Service(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public T TryCatch<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new ServiceException(MensagensErro.ErroServico, ex);
            }
        }

        public void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Entity:{0} Property: {1} Error: {2}",
            //                                    validationErrors.Entry.Entity.GetType().FullName,
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new ServiceException(MensagensErro.ErroServico, ex);
            }
        }

        public void TryCatch(Action action, bool throwException = false)
        {
            try
            {
                action();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Entity:{0} Property: {1} Error: {2}",
            //                                    validationErrors.Entry.Entity.GetType().FullName,
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (throwException)
                    throw;
                else
                    throw new ServiceException(MensagensErro.ErroServico, ex);
            }
        }

        //Valida existência de objetos relacionados ao objeto em questão
        //Método utilizado pelos testes unitários
        //public abstract void Validate()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
