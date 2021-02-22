using System.Configuration;

namespace GoltaraSolutions.SpaWeb.CompositionRoot.Configuration
{
    /// <summary>
    /// Seção de configuração customizada da Matrix
    /// Ref: https://msdn.microsoft.com/en-us/library/2tw134k3.aspx
    /// </summary>
    public class BootstrapperSection : ConfigurationSection
    {
        public static BootstrapperSection GetSection()
        {
            return (BootstrapperSection)System.Configuration.ConfigurationManager
                .GetSection("spaweb/bootstrapper");
        }

        [ConfigurationProperty("logger")]
        public LoggerConfig logger
        {
            get
            {
                return (LoggerConfig)this["logger"];
            }
            set
            { this["logger"] = value; }
        }

        [ConfigurationProperty("repository")]
        public RepositoryConfig repository
        {
            get
            {
                return (RepositoryConfig)this["repository"];
            }
            set
            { this["repository"] = value; }
        }
    }
}
// ============= app.config SAMPLE =========== //
//<?xml version="1.0" encoding="utf-8" ?>
//<configuration>
//  <configSections>
//    <sectionGroup name="spaweb">
//      <section name="bootstrapper" type="GoltaraSolutions.SpaWeb.CompositionRoot.Configuration.BootstrapperSection, GoltaraSolutions.SpaWeb.CompositionRoot" allowLocation="true" allowDefinition="Everywhere" />
//    </sectionGroup>
//  </configSections>
//  <spaweb>
//    <bootstrapper>
//      <logger strategy="Log4Net" />
//      <repository strategy="SqlServer" />
//    </bootstrapper>
//  </spaweb>
//</configuration>