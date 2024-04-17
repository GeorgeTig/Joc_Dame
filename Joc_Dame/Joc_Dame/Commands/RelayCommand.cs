using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICommandDemoAgain.Commands
{
    class RelayCommand : ICommand
    {
        /*commandTask este comanda care se executa - in cazul nostru Add, Substract, Multiply sau Divide
        Action este un delegat implicit definit in .NET Framework
        poate fi parametrizat (cu object ca in cazul de fata) sau neparametrizat
        Action este reprezentat printr-o metoda care returneaza void 
        -daca e neparametrizat, atunci metoda care il inlocuieste nu trebuie sa aiba parametru
        -daca e parametrizat(cu tipul object ca in cazul de fata), atunci metoda care il inlocuieste
        trebuie sa aiba un parametru de tipul object (vezi metodele Add, Substract, Multiply si Divide
        care au cate un parametru de tip object si returneaza void)
        In aceasta aplicatie nu aveam nevoie de parametru la metode si puteam foarte bine sa folosim
        delegatul Action fara parametrizare*/

        private Action<object> commandTask;

        public RelayCommand(Action<object> workToDo)
        {
            commandTask = workToDo;
        }
        /*CanExecute returneaza valoarea true hardcodat....se poate scrie cod, si atunci e posibil
        ca metodele reprezentate prin Action sa nu se poata executa*/

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /*Metoda Execute este responsabila de executia metodei reprezentate prin Action;
        daca Action nu era parametrizat, atunci apelul ar fi fost commandTask();
        inainte de apelul lui Execute se apeleaza automat CanExecute pentru a vedea daca metoda se
        poate executa*/
        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}