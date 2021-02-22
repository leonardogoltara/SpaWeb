using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Linq;
using GoltaraSolutions.Common.Domain;

namespace GoltaraSolutions.SpaWeb.Domain.Test.Pattern
{
    [TestClass]
    public class PatternTest
    {
        [TestMethod]
        public void ModelSetPrivado_Sucesso()
        {
            // Arrange
            var allModels = Assembly.GetAssembly(typeof(Aggregate))
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Aggregate)));

            // Act-Assert
            foreach (Type model in allModels)
            {
                //Precisa que a model tenha um construtor protected e sem parâmetros
                var objModel = Activator.CreateInstance(model, true);

                foreach (PropertyInfo prop in objModel.GetType().GetProperties())
                {
                    Assert.IsNull(prop.GetSetMethod(), $"Propriedade {prop.Name} da model {model.FullName} deve ter SET privado!");
                }
            }
        }
    }
}
