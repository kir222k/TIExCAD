using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Reflection;

// Acad
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using AdW = Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(TIExCAD.ExampleRibbon))]


namespace TIExCAD
{
    public class ExampleRibbon
    {
        //// Инициализация нашего плагина
        //public void Initialize()
        //{
        //    /* ленту грузим с помощью обработчика событий:
        //     * Этот вариант нужно использовать, если ваш плагин
        //     * стоит в автозагрузке, т.к. он (плагин) инициализируется
        //     * до построения ленты
        //     */
        //    //Autodesk.Windows.ComponentManager.ItemInitialized += new EventHandler(ComponentManager_ItemInitialized);
        //    // Т.к. мы грузим плагин через NETLOAD, то строим вкладку в ленте сразу
        //    BuildRibbonTab();
        //}
        //// Происходит при закрытии автокада
        //public void Terminate()
        //{
        //    // Тут в принципе ничего не требуется делать
        //}


        /* Обработчик события
         * Следит за событиями изменения окна автокада.
         * Используем его для того, чтобы "поймать" момент построения ленты,
         * учитывая, что наш плагин уже инициализировался
         */

        /* перенесено в Init.cs
        public  void ComponentManager_ItemInitialized(object sender, Autodesk.Windows.RibbonItemEventArgs e)
        {
            // Проверяем, что лента загружена
            if (Autodesk.Windows.ComponentManager.Ribbon != null)
            {
                // Строим нашу вкладку
                BuildRibbonTab();
                //и раз уж лента запустилась, то отключаем обработчик событий
                Autodesk.Windows.ComponentManager.ItemInitialized -= new EventHandler<RibbonItemEventArgs>(ComponentManager_ItemInitialized);
            }
        }
        // Построение вкладки
        public void BuildRibbonTab()
        {
            // Если лента еще не загружена
            if (!isLoaded())
            {
                // Строим вкладку
                CreateRibbonTab();
                // Подключаем обработчик событий изменения системных переменных
                acadApp.SystemVariableChanged += new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
            }
        }
        */

        // Проверка "загруженности" ленты
        public bool isLoaded()
        {
            bool _loaded = false;
            RibbonControl ribCntrl = Autodesk.Windows.ComponentManager.Ribbon;
            // Делаем итерацию по вкладкам ленты
            foreach (RibbonTab tab in ribCntrl.Tabs)
            {
                // И если у вкладки совпадает идентификатор и заголовок, то значит вкладка загружена
                if (tab.Id.Equals("RibbonExample_ID") & tab.Title.Equals("RibbonExample"))
                { _loaded = true; break; }
                else _loaded = false;
            }
            return _loaded;
        }


        /* Удаление своей вкладки с ленты
         * В данном примере не используем
         */

        /*
        [CommandMethod("RibDel")]
        public  void RemoveRibbonTab()
        {
            try
            {
                RibbonControl ribCntrl = Autodesk.Windows.ComponentManager.Ribbon;
                // Делаем итерацию по вкладкам ленты
                foreach (RibbonTab tab in ribCntrl.Tabs)
                {
                    if (tab.Id.Equals("RibbonExample_ID") & tab.Title.Equals("RibbonExample"))
                    {
                        // И если у вкладки совпадает идентификатор и заголовок, то удаляем эту вкладку
                        ribCntrl.Tabs.Remove(tab);
                        // Отключаем обработчик событий
                        acadApp.SystemVariableChanged -= new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
                        // Останавливаем итерацию
                        break;
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        /* Обработка события изменения системной переменной
         * Будем следить за системной переменной WSCURRENT (текущее рабочее пространство),
         * чтобы наша вкладка не "терялась" при изменение рабочего пространства
         */

        /*
        public  void acadApp_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (e.Name.Equals("WSCURRENT")) BuildRibbonTab();
        }
        */

        // Создание нашей вкладки
        [CommandMethod("RibCreate")]
        public void CreateRibbonTab()
        {
            try
            {

                AcadSendMess AcSM = new AcadSendMess();
                AcSM.SendStringDebugStars(new List<string> { "CreateRibbonTab", "Заход  try" });

                // Получаем доступ к ленте
                RibbonControl ribCntrl = Autodesk.Windows.ComponentManager.Ribbon;
                AcSM.SendStringDebugStars(new List<string> { "CreateRibbonTab", "Получаем доступ к ленте" });


                // добавляем свою вкладку
                RibbonTab ribTab = new RibbonTab();
                AcSM.SendStringDebugStars(new List<string> { "CreateRibbonTab", "добавляем свою вкладку" });


                ribTab.Title = "RibbonExample"; // Заголовок вкладки
                ribTab.Id = "RibbonExample_ID"; // Идентификатор вкладки
                ribCntrl.Tabs.Add(ribTab); // Добавляем вкладку в ленту
                // добавляем содержимое в свою вкладку (одну панель)
                addExampleContent(ribTab);
                // Делаем вкладку активной (не желательно, ибо неудобно)
                //ribTab.IsActive = true;
                // Обновляем ленту (если делаете вкладку активной, то необязательно)
                ribCntrl.UpdateLayout();
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        // Строим новую панель в нашей вкладке
        public void addExampleContent(RibbonTab ribTab)
        {
            try
            {
                // создаем panel source
                RibbonPanelSource ribSourcePanel = new RibbonPanelSource();
                ribSourcePanel.Title = "RibbonExample";
                // теперь саму панель
                RibbonPanel ribPanel = new RibbonPanel();
                ribPanel.Source = ribSourcePanel;
                ribTab.Panels.Add(ribPanel);
                // создаем пустую tooltip (всплывающая подсказка)
                RibbonToolTip tt;
                // создаем split button
                RibbonSplitButton risSplitBtn = new RibbonSplitButton();
                /* Для RibbonSplitButton ОБЯЗАТЕЛЬНО надо указать
                 * свойство Text, а иначе при поиске команд в автокаде
                 * будет вылетать ошибка.
                 */
                risSplitBtn.Text = "RibbonSplitButton";
                // Ориентация кнопки
                risSplitBtn.Orientation = System.Windows.Controls.Orientation.Vertical;
                // Размер кнопки
                risSplitBtn.Size = RibbonItemSize.Large;
                // Показывать изображение
                risSplitBtn.ShowImage = true;
                // Показывать текст
                risSplitBtn.ShowText = true;
                // Стиль кнопки
                risSplitBtn.ListButtonStyle = Autodesk.Private.Windows.RibbonListButtonStyle.SplitButton;
                risSplitBtn.ResizeStyle = RibbonItemResizeStyles.NoResize;
                risSplitBtn.ListStyle = RibbonSplitButtonListStyle.List;
                /* Далее создаем две кнопки и добавляем их
                 * не в панель, а в RibbonSplitButton
                 */
                #region Кнопка-пример №1
                // Создаем новый экземпляр подсказки
                tt = new RibbonToolTip();
                // Отключаем вызов справки (в данном примере её нету)
                tt.IsHelpEnabled = false;
                // Создаем кнопку
                RibbonButton ribBtn = new RibbonButton();
                /* В свойство CommandParameter (параметры команды)
                 * и в свойство Command (отображает команду) подсказки
                 * пишем вызываемую команду
                 */
                ribBtn.CommandParameter = tt.Command = "_Line";
                // Имя кнопки
                ribBtn.Name = "ExampleButton1";
                // Заголовок кнопки и подсказки
                ribBtn.Text = tt.Title = "Кнопка-пример №1";
                // Создаем новый (собственный) обработчик команд (см.ниже)
                ribBtn.CommandHandler = new RibbonCommandHandler();
                // Ориентация кнопки
                ribBtn.Orientation = System.Windows.Controls.Orientation.Horizontal;
                // Размер кнопки
                ribBtn.Size = RibbonItemSize.Large;

                /* Т.к. используем размер кнопки Large, то добавляем
                 * большое изображение с помощью специальной функции (см.ниже)
                 */
                ribBtn.LargeImage = LoadImage("icon_32");

                // Показывать картинку
                ribBtn.ShowImage = true;


                // Показывать текст
                ribBtn.ShowText = true;
                // Заполняем содержимое всплывающей подсказки
                tt.Content = "Я кнопочка №1. Нажми меня и я нарисую отрезок";
                // Подключаем подсказку к кнопке
                ribBtn.ToolTip = tt;
                // Добавляем кнопку в RibbonSplitButton
                risSplitBtn.Items.Add(ribBtn);
                #endregion
                // Делаем текущей первую кнопку
                risSplitBtn.Current = ribBtn;
                // Далее создаем вторую кнопку по аналогии с первой
                #region Кнопка-пример №2
                tt = new RibbonToolTip();
                tt.IsHelpEnabled = false;
                ribBtn = new RibbonButton();
                ribBtn.CommandParameter = tt.Command = "_Pline";
                ribBtn.Name = "ExampleButton2";
                ribBtn.Text = tt.Title = "Кнопка-пример №2";
                ribBtn.CommandHandler = new RibbonCommandHandler();
                ribBtn.Orientation = System.Windows.Controls.Orientation.Horizontal;
                ribBtn.Size = RibbonItemSize.Large;

                ribBtn.LargeImage = LoadImage("icon_32");
                ribBtn.ShowImage = true;

                ribBtn.ShowText = true;
                tt.Content = "Я кнопочка №2. Нажми меня и я нарисую полилинию";
                ribBtn.ToolTip = tt;
                risSplitBtn.Items.Add(ribBtn);
                #endregion
                // Добавляем RibbonSplitButton в нашу панель
                ribSourcePanel.Items.Add(risSplitBtn);
                // Создаем новую строку
                RibbonRowPanel ribRowPanel = new RibbonRowPanel();
                // Создаем третью кнопку по аналогии с предыдущими.
                // Отличие только в размере кнопки (и картинки)
                #region Кнопка-пример №3
                tt = new RibbonToolTip();
                tt.IsHelpEnabled = false;
                ribBtn = new RibbonButton();
                ribBtn.CommandParameter = tt.Command = "_Circle";
                ribBtn.Name = "ExampleButton3";
                ribBtn.Text = tt.Title = "Кнопка-пример №3";
                ribBtn.CommandHandler = new RibbonCommandHandler();
                ribBtn.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribBtn.Size = RibbonItemSize.Standard;

                //ribBtn.Image = LoadImage("icon_16");


                ribBtn.ShowImage = false;
                ribBtn.ShowText = false;
                tt.Content = "Я кнопочка №3. Нажми меня и я нарисую кружочек";
                ribBtn.ToolTip = tt;
                ribRowPanel.Items.Add(ribBtn);
                #endregion
                // Добавляем строку в нашу панель
                ribSourcePanel.Items.Add(ribRowPanel);
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }
        // Получение картинки из ресурсов
        // Данная функция найдена на просторах интернет
        System.Windows.Media.Imaging.BitmapImage LoadImage(string ImageName)
        {
            return new System.Windows.Media.Imaging.BitmapImage(
                new Uri("pack://application:,,,/ACadRibbon;component/" + ImageName + ".png"));
        }
        /* Собственный обраотчик команд
         * Это один из вариантов вызова команды по нажатию кнопки
         */
        public class RibbonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;
            public void Execute(object parameter)
            {
                Document doc = acadApp.DocumentManager.MdiActiveDocument;
                if (parameter is RibbonButton)
                {
                    // Просто берем команду, записанную в CommandParameter кнопки
                    // и выпоняем её используя функцию SendStringToExecute
                    RibbonButton button = parameter as RibbonButton;
                    acadApp.DocumentManager.MdiActiveDocument.SendStringToExecute(
                        button.CommandParameter + " ", true, false, true);
                }
            }
        }
    }

    /// <summary>
    /// Создание вкладки на ленте. Основной метод возращает настроенную вклвдку, кот. затем можно встроить в ленту Autodesk.Windows.ComponentManager.Ribbon.Tabs.Add  
    /// </summary>
    public class RibbonCreateEasy
    {
        // ПОЛЯ

        // СВОЙСТВА


        private string pathImgFolder;
        /// <value>
        /// Путь к папке img, где лежат png файлы картинок на кнопки, размеры - 32, 16 для кажд. кнопки.
        /// </value>
        public string PathImgFolder
        {
            set
            {
                if (Directory.Exists(value))
                {
                    pathImgFolder = value;
                }
                else
                {
                    pathImgFolder = null;
                }
            }
        }


        // КОНСТРУКТОРЫ

        // МЕТОДЫ

        /// <summary>
        /// Создание вкладки ленты, пустой, нужно добавить панель(панели), в напели добавить кнопки.
        /// </summary>
        /// <param name="ribTitle">Заголовок вкладки ленты</param>
        /// <param name="ribID">ID вкладки ленты</param>
        /// <returns>Ссылка на объект "Вкладка ленты"</returns>
        public RibbonTab GetRibTab(string ribTitle, string ribID)
        {
            try
            {
                // добавляем свою вкладку
                RibbonTab ribTab = new RibbonTab()
                {
                    Title = ribTitle,
                    Id = ribID
                };

                return ribTab;
            }
            catch (System.Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Создание Панели вкладки ленты, пустой, нужно еще добавить кнопки. Саму панель нужно вставить во Вкладку ribTab.Panels.Add(ribPanel).
        /// </summary>
        /// <param name="ribPanelTitle"></param>
        /// <returns>Ссылка на объект "Панель кладки ленты"</returns>
        public RibbonPanel GetRibPanel(string ribPanelTitle)
        {
            try
            {
                // создаем panel source
                RibbonPanelSource ribSourcePanel = new RibbonPanelSource()
                {
                    Title = ribPanelTitle
                };

                // теперь саму панель
                RibbonPanel ribPanel = new RibbonPanel()
                {
                    Source = ribSourcePanel
                };

                return ribPanel;
            }

            catch (System.Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Создание кнопки с картинками
        /// </summary>
        /// <param name="ribButtonText"></param>
        /// <param name="showText"></param>
        /// <param name="showImage"></param>
        /// <param name="ribButtonLargeImage"></param>
        /// <param name="ribButtonImage"></param>
        /// <returns>Ссылка на объект кнопки с картинками</returns>
        public RibbonButton GetRibButton(string ribButtonText, bool showText, 
            RibbonItemSize ribButtonSize, Orientation ribButtonOrientation,
            bool showImage,
            string ribButtonLargeImageName = "image_32.png",
            string ribButtonImageName = "image_16.png") // аргумент не обязательный
        {
                // Создадим объект кнопки.
                AdW.RibbonButton ribButton = new RibbonButton();

            try
            {
                #region
                /*
                create the buttons listed in the split button

                Autodesk.Windows.RibbonButton button1 = new RibbonButton();

                button1.Text = "Button1";

                button1.ShowText = true;

                button1.ShowImage = true;

                button1.LargeImage = getBitmap("a_large.png");

                button1.Image = getBitmap("a_small.png");

                button1.CommandHandler = new RibButtonHandler();
                */
                #endregion

                // Проверка на существование путей к файлам картинок.
                string pathDirIm;
                // Проверим путь к папке img.
                if (pathImgFolder == null || pathImgFolder == String.Empty) // если свойство PathImgFolder не задано.
                {
                    // присвоим путь по умолчанию.
                    // путь к папке, откуда запущена Dll.
                    DirectoryInfo dirDLL = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
                    // Проверим, что в <InstallDir> есть папка img.
                    pathDirIm = $"{dirDLL.Parent.Parent.Parent.ToString()}/img/";
                }
                else // если свойство PathImgFolder задано, то
                {
                    pathDirIm = this.pathImgFolder; // присвоим тот путь, кот. задали сами
                }

                // Теперь займемся файлами картинок.
                if (Directory.Exists(pathDirIm)) // еще раз проверяем сущ пути к папке картинок
                {
                    // получим объект для получения полного пути.
                    DirectoryInfo dirIMG = new DirectoryInfo(pathDirIm); 
                    // присвоим путь к папке <InstallDir>/img.
                    pathImgFolder = dirIMG.FullName;
                    // получим путь к файлу большой картинки.
                    string pathImg32 = pathImgFolder + ribButtonLargeImageName;
                    // получим путь к файлу малой картинки.
                    string pathImg16 = pathImgFolder + ribButtonImageName;
                    // Проверим существование картинок в папке IMG.
                    if (File.Exists(pathImg32) && File.Exists(pathImg16))
                    {
                        // создадим объекты BitmapImage
                        BitmapImage bitmapImage32 = new BitmapImage(new Uri(pathImg32)); // путь к большой картинке
                        BitmapImage bitmapImage16 = new BitmapImage(new Uri(pathImg16));
                        // создадим картинки
                        // Uri uri1 = new Uri("C:/test/path/file.txt") // Implicit file path.
                        // Если файл рисунка лежит рядом с dll: Assembly.GetExecutingAssembly().Location.
                        // или
                        // ribButtonBig.LargeImage = LoadImage(My.Resources.question) - но нужен еще метод https://adndevblog.typepad.com/autocad/2012/05/arrange-ribbon-buttons-into-columns.html

                        // Метод, создающий кнопку.
                        // RbB1 = RibCrEsy.GetRibButton("ОДИН ОДИНОДИН ОДИН ОДИН ", true, true,
                        // new BitmapImage(new Uri("u:/dev/TIExCAD/distr/lib/ac15/icon_32.png")), // путь к большой картинке
                        // new BitmapImage(new Uri("u:/dev/TIExCAD/distr/lib/ac15/icon_16.png")),  // путь к мал. картинке

                        ribButton.Image = bitmapImage16;
                        ribButton.LargeImage = bitmapImage32;
                        ribButton.ShowImage = showImage;

                    }
                    else // Если картинок для кнопки так и не найдено, отключим их прорисовку
                    {
                        //ribButton.Image = bitmapImage16;
                        //ribButton.LargeImage = bitmapImage32;
                        ribButton.ShowImage = false;
                    }

                }
                else // если какой-то сбой и пути к картинкам нет
                {
                    ribButton.ShowImage = false;
                }

                // С картинками закончено, настроим др. свойства кнопки.
                ribButton.Text = ribButtonText;
                ribButton.ShowText = showText;
                ribButton.Size= ribButtonSize;
                ribButton.Orientation = ribButtonOrientation;

                return ribButton;
            }

            #region CATCH
            catch (System.Exception)
            {
                
                //throw;
                AcadSendMess AcSM = new AcadSendMess();
                AcSM.SendStringDebugStars(new List<string>
                {$"Создание кнопки {ribButtonText} прервано", "Класс:", $"{this}", "Метод:", "GetRibButton"});

                return null;
            }
            #endregion
        }
    
        
        ///// <summary>
        ///// Создание кнопки без картинки
        ///// </summary>
        ///// <param name="ribButtonText">Текст кнопки</param>
        ///// <returns>Ссылка на объект кнопки</returns>
        //public RibbonButton GetRibButton(string ribButtonText, RibbonItemSize ribButtonSize, Orientation ribButtonOrientation)
        //{
        //    try
        //    {
        //        AdW.RibbonButton ribButton = new RibbonButton()
        //        {
        //            //Text
        //            Text = ribButtonText,
        //            //ShowText
        //            ShowText = true,
        //            //ShowImage
        //            ShowImage = false,
        //            // 
        //            Size = ribButtonSize,
        //            // 
        //            Orientation = ribButtonOrientation,
        //            //
        //            //ResizeStyle = RibbonItemResizeStyles.ResizeWidth
        //        };

        //        return ribButton;
        //    }

        //    catch (System.Exception)
        //    {
        //        return null;
        //        throw;
        //    }
        //}

    }

    /// <summary>
    /// Содержит переопределяемый метод, кот. выполняется при нажатии кнопки на ленте.
    /// Экз. данного класса (или его наследника) присваивается свойству CommandHandler кнопки ленты.
    /// Для использования в основном коде, чтобы использовать свои методы как обработчики, 
    /// нужно создать класс-наследник от RibButtonHandler, переопределить метод Execute
    /// </summary>
    public class RibButtonHandler : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Определен в интерфейсе  System.Windows.Input.ICommand, не используется.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Событие на нажатие кнопки
        /// </summary>
        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            Document doc = acadApp.DocumentManager.MdiActiveDocument;

            if (parameter is RibbonButton)
            {
                RibbonButton button = parameter as RibbonButton;
                doc.Editor.WriteMessage(
                  "\nRibbonButton Executed: " + button.Text + "\n");
            }
        }
    }


}
