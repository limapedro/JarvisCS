using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai tratar de ajudar os usuários e programadores
    /// </summary>
    public class JARVISHelp
    {
        // Introdução ao assistente
        public static void Introduction()
        {
            Speaker.Speak("jarvis é o assistente do homem de ferro... como uma inteligência artiificial... ");
            Speaker.Speak(" ele é muito útil para o seu dono e criador... agora imagine se alguém, assim como você, tivesse o jarvis... ");
            Speaker.Speak(" ou outro assistente de outro nome... as possibilidades são muitas... felizmente.. muitos caras... ao redor do mundo ");
            Speaker.Speak(" Estados Unidos... India... Brasil... já tentaram criar um jarvis.. mas poucos tiveram oportunidade de obter... e ainda com muitos poucos ");
            Speaker.Speak(" comandos, e recursos... junte-se à nós e vamos criar um assistente virtual para a plataforma Windows... para que todos possam... aprender o que é ");
            Speaker.Speak(" inteligência artificial.. e de como a tecnologia pode mudar o mundo... eu sou jarvis e esse é o meu recado para você...");
        }
    }
}
