using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using MongoDB.Bson;

using Realms;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Winui3ItemsControlBug
{

    public class User: RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id
        {
            get; set;
        } = ObjectId.GenerateNewId();

        public string Firstname { get; set; }
        public string LastName { get; set; }


        public User()
        {
            
        }

        public User(string firstname, string lastName)
        {
            Firstname = firstname;
            LastName = lastName;
        }
    }
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

       private ObservableCollection<User> Users = new ();

        private Realm _db;

        private IQueryable<User> _users;
        public MainWindow()
        {
            var cfg = new InMemoryConfiguration("realm_mem01");

            _db = Realm.GetInstance(cfg);

            _users = _db.All<User>();

            this.InitializeComponent();
        }

       
        private async void ItemsControl1_Loaded(object sender, RoutedEventArgs e)
        {
            await _db.WriteAsync( () =>
            {
                _db.Add(new User("Jhon", "Red"));
                _db.Add(new User("Mike", "Blue"));
            });

          
        }
    }
}
