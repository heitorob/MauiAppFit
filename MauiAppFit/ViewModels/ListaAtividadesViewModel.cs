using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiAppFit.Models;

namespace MauiAppFit.ViewModels
{
    internal class ListaAtividadesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ParametroBusca { get; set; }

        bool estaAtualizando = false;
        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }

        ObservableCollection<Atividade> listaAtividades =
            new ObservableCollection<Atividade>();

        public ObservableCollection<Atividade> ListaAtividades
        {
            get => listaAtividades;
            set => listaAtividades = value;
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando) return;

                        EstaAtualizando = true;

                        List<Atividade> tmp = await App.Database.GetAll();

                        ListaAtividades.Clear();
                        tmp.ForEach(i => ListaAtividades.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }
    }
}
