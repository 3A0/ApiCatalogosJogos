using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Exceptions
{
    public class JogoJaCadastradoException
    {
        public JogoJaCadastradoException()
            : base(" Este ja jogo está cadastrado")
        { }
    }
}
