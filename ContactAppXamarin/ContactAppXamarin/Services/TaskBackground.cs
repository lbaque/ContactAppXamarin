using ContactAppXamarin.Database;
using ContactAppXamarin.Domain;
using ContactAppXamarin.Helpers;
using ContactAppXamarin.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactAppXamarin.Services
{
    public interface ITaskBackground
    {
        void Start();
    }
    public class TaskBackground : ITaskBackground
    {
        private IHttpClientUris _uri;
        private ContactoDatabase _database;
        private Task[] _tasks = null;

        public TaskBackground()
        {
            _uri = DependencyService.Get<IHttpClientUris>();
            _database = new ContactoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactAppXamarin.db3"));
        }

        
        public void Start()
        {
            _tasks = new Task[3] {
                new Task(async ()=> await PostContacto()),
                new Task(async ()=> await GetContacto()),
                new Task(async ()=> await DeleteContacto())
            };


            foreach (var _task in _tasks)
                _task.Start();



        }
        private async Task PostContacto()
        {
            while (true)
            {
                try
                {
                    var data = await _database.GetAllAsync(x => x.synchronized == false);
                    foreach (var item in data)
                    {
                        var http = _uri.ContactApi();
                        string uri = ServiceRoute.Contact.V1.Contacto.FetchId(item.Id);
                        var exists = (await http.GetAsync(uri)).StatusCode == System.Net.HttpStatusCode.OK;

                        HttpResponseMessage response = null;
                        if (exists)
                            response = await http.PutAsJsonAsync(uri, item);
                        else
                            response = await http.PostAsJsonAsync(ServiceRoute.Contact.V1.Contacto.Fetch, item);

                        if (response.IsSuccessStatusCode)
                        {
                            item.synchronized = true;
                            await _database.UpdateAsync(item);
                        }                 

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Thread.Sleep(new TimeSpan(0, 0, 10));
                }
            }
        }

        private async Task GetContacto()
        {
            while (true)
            {
                try
                {
                    var contactos = await _database.GetAllAsync();
                    if (contactos.Count == 0 && Application.Current.Properties.ContainsKey("User"))
                    {
                        string UsertId = (Application.Current.Properties["UserId"] as string);

                        var http = _uri.ContactApi();
                        string uri = ServiceRoute.Contact.V1.Contacto.Fetch;
                        uri = $"{uri}?UsuarioId={UsertId}";
                        var data = await http.GetFromJsonAsync<List<ContactoModel>>(uri);
                        data.ForEach(x => x.synchronized = true);
                        await _database.InsertAllAsync(data);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Thread.Sleep(new TimeSpan(0, 0, 10));

                }

            }

        }

        private async Task DeleteContacto()
        {
            while (true)
            {
                try
                {
                    var contactos = await _database.GetAllAsync(x => x.Deleted);
                    foreach (var item in contactos)
                    {
                        var http = _uri.ContactApi();
                        await http.DeleteAsync(ServiceRoute.Contact.V1.Contacto.FetchId(item.Id));
                    }
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Thread.Sleep(new TimeSpan(0, 0, 10));

                }

            }

            
        }
    }
}
