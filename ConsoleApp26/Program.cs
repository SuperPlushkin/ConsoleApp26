using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace program
{
    public interface IBorrowable
    {
        void Borrow();
        void Return();
    }
    public abstract class LibraryItem<T> : IBorrowable
    {
        public string title;
        public int year;
        public int id;
        public bool IsAvailable;

        public LibraryItem()
        {
            title = "Nigga";
            year = 1999;
            id = 1234;
            IsAvailable = true;
        }
        
        public void DisplayInfo(LibraryItem<User> item)
        {
            Console.WriteLine($"Info about {item.GetType().Name}:");
            Console.WriteLine("Name --> " + title);
            Console.WriteLine("Year --> " + year);
            Console.WriteLine("Id --> " + id);
            Console.WriteLine("Available --> " + IsAvailable);
            Console.WriteLine("");
        }
        public virtual void Borrow()
        {

        }
        public virtual void Return()
        {

        }
    }
    public class Book : LibraryItem<User>, IBorrowable 
    {
        public Book() { }
        public Book(string title_, int year_, int id_)
        {
            title = title_;
            year = year_;
            id = id_;
            IsAvailable = true;
        }
        public override void Borrow()
        {
            IsAvailable = false;
            Console.WriteLine("The RENTAL was successful.");
            Console.WriteLine("The Rented Book info:");
            Console.WriteLine($"Title --> {title}");
            Console.WriteLine($"Id --> {id}");
            Console.WriteLine($"Year --> {year}");
            Console.WriteLine($"Available --> {IsAvailable}");
            Console.WriteLine("");
        }
        public override void Return()
        {
            IsAvailable = true;
            Console.WriteLine("The RETURN was successful.");
            Console.WriteLine("The Rented Book info:");
            Console.WriteLine($"Title --> {title}");
            Console.WriteLine($"Id --> {id}");
            Console.WriteLine($"Year --> {year}");
            Console.WriteLine($"Available --> {IsAvailable}");
            Console.WriteLine("");
        }
    }
    public class Journal : LibraryItem<User>, IBorrowable
    {
        public Journal(){}
        public Journal(string title_, int year_, int id_)
        {
            title = title_;
            year = year_;
            id = id_;
            IsAvailable = true;
        }
        public override void Borrow()
        {
            IsAvailable = false;
            Console.WriteLine("The RENTAL was successful.");
            Console.WriteLine("The Rented Journal info:");
            Console.WriteLine($"Title --> {title}");
            Console.WriteLine($"Id --> {id}");
            Console.WriteLine($"Year --> {year}");
            Console.WriteLine($"Available --> {IsAvailable}");
            Console.WriteLine("");
        }
        public override void Return()
        {
            IsAvailable = true;
            Console.WriteLine("The RETURN was successful.");
            Console.WriteLine("The Returned Journal info:");
            Console.WriteLine($"Title --> {title}");
            Console.WriteLine($"Id --> {id}");
            Console.WriteLine($"Year --> {year}");
            Console.WriteLine($"Available --> {IsAvailable}");
            Console.WriteLine("");
        }
    }
    public class User
    {
        List<LibraryItem<User>> borrowed_items;
        public string name { get; set; }
        public int id { get; }
        public User()
        {

            borrowed_items = new List<LibraryItem<User>>();
            name = "Kirill";
            id = 2;
        }
        public User(string name_, int id_)
        {
            borrowed_items = new List<LibraryItem<User>>();
            name = name_;
            id = id_;
        }
        public List<LibraryItem<User>> GetBorrowedItems()
        {
            return borrowed_items;
        }
        public void BorrowItem(List<LibraryItem<User>> libr, LibraryItem<User> item)
        {
            bool IsFind = false;
            foreach (var item_libr in libr)
            {
                if (item_libr.id == item.id)
                {
                    IsFind = true;
                    item_libr.Borrow();
                    borrowed_items.Add(item);
                    break;
                }
            }
            if (!IsFind)
            {
                Console.WriteLine("Такой книги нет");
            }
        }
        public void ReturnItem(LibraryItem<User> item)
        {
            bool IsFind = false;
            foreach (var book in borrowed_items)
            {
                if (book.id == item.id)
                {
                    if (!book.IsAvailable)
                    {
                        IsFind = true;
                        book.Return();
                        borrowed_items.Remove(book);
                        break;
                    }
                }
            }
            if (!IsFind)
            { 
                Console.WriteLine("There is no such book or it is impossible to rent it");
            }
        }
    }
    public class Library
    {
        private List<LibraryItem<User>> libr_items;
        public Library()
        {
            libr_items = new List<LibraryItem<User>>();
        }
        public void AddItem(LibraryItem<User> item)
        {
            libr_items.Add(item);
            Console.WriteLine("");
            Console.WriteLine($"Added {item.GetType().Name} <<{item.title}>> to the library");
        }
        public void DeleteItem(LibraryItem<User> item)
        {
            libr_items.Remove(item);
            Console.WriteLine("");
            Console.WriteLine($"Deleted {item.GetType().Name} <<{item.title}>> from the library");
        }
        public void ListAvailableItems()
        {
            int count = 1;
            Console.WriteLine("");
            Console.WriteLine("Available items:");
            foreach (var item in libr_items)
            {
                if (item.IsAvailable)
                {
                    Console.WriteLine("");
                    Console.Write(count + ") ");
                    item.DisplayInfo(item);
                    count++;
                }
            }
            if(count == 1)
            {
                Console.WriteLine("List of Available Items is empty!!!");
            }
        }
        public void BorrowItem(User user, LibraryItem<User> item)
        {
            user.BorrowItem(libr_items, item);
        }
        public void ReturnItem(User user, LibraryItem<User> item)
        {
            user.ReturnItem(item);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Я если что сам все делал

            Library libr = new Library();
            List<User> users = new List<User>();
            Random rnd = new Random();
            User borrower = new User();

            while (true)
            {
                int option = 0;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("1. Add User");
                        Console.WriteLine("2. Add Book");
                        Console.WriteLine("3. Add Journal");
                        Console.WriteLine("4. List Available Items");
                        Console.WriteLine("5. Borrow Item");
                        Console.WriteLine("6. Return Item");
                        Console.WriteLine("7. List Borrowed Items");
                        Console.WriteLine("8. Exit");
                        Console.Write("Choose an option: ");
                        option = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Error. Try again. Please type number");
                        Console.WriteLine("");
                    }
                }
                switch (option)
                {
                    case 1:
                        Console.Write("Enter user name: ");
                        string userName = Console.ReadLine();
                        User user = new User(userName, rnd.Next(10000, 9999999));
                        users.Add(user);
                        Console.WriteLine($"User {userName} added with id --> {user.id}.");
                        break;
                    case 2:
                        Console.Write("Enter book title --> ");
                        string bookTitle = Console.ReadLine();
                        Console.Write("Enter book year --> ");
                        int bookYear = 0;
                        try
                        {
                            bookYear = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Error. Try again. Please type number");
                            break;
                        }
                        Book book = new Book(bookTitle, bookYear, rnd.Next(10000, 9999999));
                        libr.AddItem(book);
                        break;
                    case 3:
                        Console.Write("Enter journal title --> ");
                        string journalTitle = Console.ReadLine();
                        Console.Write("Enter journal year --> ");
                        int journalYear = 0;
                        try
                        {
                            journalYear = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Error. Try again. Please type number");
                            break;
                        }
                        Journal journal = new Journal(journalTitle, journalYear, rnd.Next(10000, 9999999));
                        libr.AddItem(journal);
                        break;
                    case 4:
                        libr.ListAvailableItems();
                        break;
                    case 5:
                        Console.Write("Enter user ID: ");
                        int userId = 0;
                        try
                        {
                            userId = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Error. Try again. Please type number");
                            break;
                        }
                        
                        bool IsFind = false;
                        foreach(User user_ in users)
                        {
                            if(user_.id == userId)
                            {
                                borrower = user_;
                                IsFind = true;
                            }
                        }

                        if (IsFind)
                        {
                            Console.Write("Enter item ID: ");
                            int itemId = 0;
                            try
                            {
                                itemId = Convert.ToInt32(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Error. Try again. Please type number");
                                break;
                            }

                            bool ItemFind = false;
                            foreach (var item_ in borrower.GetBorrowedItems())
                            {
                                if (item_.id == itemId)
                                {
                                    var item = item_;
                                    if (item != null)
                                    {
                                        libr.BorrowItem(borrower, item);
                                        ItemFind = true;
                                    }
                                    
                                    break;
                                }
                            }
                            if(!ItemFind)
                            {
                                Console.WriteLine("Item not found.");
                            }
                            borrower = new User();
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;
                    case 6:
                        Console.Write("Enter user ID: ");
                        userId = int.Parse(Console.ReadLine());

                        foreach (var user_ in users)
                        {
                            if (user_.id == userId)
                            {
                                Console.Write("Enter item ID: ");
                                int itemId = int.Parse(Console.ReadLine());
                                bool ItemFind = false;

                                foreach (var item_ in borrower.GetBorrowedItems())
                                {
                                    if (item_.id == itemId)
                                    {
                                        var item = item_;
                                        if (item != null)
                                        {
                                            libr.BorrowItem(borrower, item);
                                            ItemFind = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Item not found.");
                                        }
                                        break;
                                    }
                                }

                                if (!ItemFind)
                                {
                                    Console.WriteLine("Item not found.");
                                }
                                borrower = new User();
                                break;
                            }
                        }

                        if (borrower == null)
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;
                    case 7:
                        Console.Write("Enter user ID: ");
                        userId = int.Parse(Console.ReadLine());

                        foreach (var user_ in users)
                        {
                            if (user_.id == userId)
                            {
                                var list = borrower.GetBorrowedItems();
                                foreach (var item in list)
                                {
                                    item.DisplayInfo(item);
                                }
                                break;
                            }
                        }

                        if (borrower == null)
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("");
            }
        }
    }
}
