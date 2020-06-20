using System;
using System.Threading.Tasks;

namespace Testes_Delegate
{
    public  class Task
    {
        public delegate void DelegateEscreveLinha(string messageLog);
        public event DelegateEscreveLinha EscreveLinha;


        private void OnEscreveLinha(string messageLog)
        {
            if (EscreveLinha != null)
                EscreveLinha(messageLog);
        }

        public  Coffee PourCoffee()
        {
            OnEscreveLinha("Coando Café");
            return new Coffee();
        }

        public  Egg FryEggs(int howMany)
        {
            OnEscreveLinha("Esquentando a Panela...");
            System.Threading.Tasks.Task.Delay(3000).Wait();
            OnEscreveLinha($"Quebrando {howMany} ovos");
            OnEscreveLinha("fritando os ovos ...");
            System.Threading.Tasks.Task.Delay(3000).Wait();
            OnEscreveLinha("Colocando os Ovos No prato");

            return new Egg();
        }

        public async Task<Egg> FryEggsAsync(int howMany)
        {
            OnEscreveLinha("Esquentando a Panela...");
            await System.Threading.Tasks.Task.Delay(3000);
            OnEscreveLinha($"Quebrando {howMany} ovos");
            OnEscreveLinha("fritando os ovos ...");
            await System.Threading.Tasks.Task.Delay(3000);
            OnEscreveLinha("Colocando os Ovos No prato");

            return new Egg();
        }

        public  Bacon FryBacon(int slices)
        {
            OnEscreveLinha($"Colocando {slices} fatias de bacon na panela");
            OnEscreveLinha("Fritando primeiro lado do bacon");
            System.Threading.Tasks.Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                OnEscreveLinha("Virando a fatia de bacon");
            }
            OnEscreveLinha("Fritando o segundo lado do bacon ...");
            System.Threading.Tasks.Task.Delay(3000).Wait();
            OnEscreveLinha("Colocando o Bacon no prato");

            return new Bacon();
        }

        public async Task<Bacon> FryBaconAsync(int slices)
        {
            OnEscreveLinha($"Colocando {slices} fatias de bacon na panela");
            OnEscreveLinha("Fritando primeiro lado do bacon");
            await System.Threading.Tasks.Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                OnEscreveLinha("Virando a fatia de bacon");
            }
            OnEscreveLinha("Fritando o segundo lado do bacon ...");
            await System.Threading.Tasks.Task.Delay(3000);
            OnEscreveLinha("Colocando o Bacon no prato");

            return new Bacon();
        }

        public  Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                OnEscreveLinha("Colocando a fatia de torrada na torradeira");
            }
            OnEscreveLinha("Ligando a torradeira");
            System.Threading.Tasks.Task.Delay(3000).Wait();
            OnEscreveLinha("Retirando a torrada da torradeira");

            return new Toast();
        }

        public async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                OnEscreveLinha("Colocando a fatia de torrada na torradeira");
            }
            OnEscreveLinha("Ligando a torradeira");
            await System.Threading.Tasks.Task.Delay(3000);
            OnEscreveLinha("Retirando a torrada da torradeira");

            return new Toast();
        }

        public async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        public  Juice PourOJ()
        {
            OnEscreveLinha("Coando o suco de laranja");
            return new Juice();
        }

        public  void ApplyJam(Toast toast) =>
            OnEscreveLinha("Colocando geleia na torrada");

        public  void ApplyButter(Toast toast) =>
            OnEscreveLinha("Colocando Geleia na Torrada");


    }

    public class Juice
    {

    }

    public class Toast
    {

    }

    public class Bacon
    {

    }

    public class Egg
    {

    }
    public class Coffee
    {

    }




}
