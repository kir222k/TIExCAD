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

[assembly: CommandClass(typeof(TIExCAD.Generic.Ribbon.ExampleRibbon))]


namespace TIExCAD.Generic.Ribbon
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
                //ribbonTab.IsActive = true;
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

        // СВОЙСТВА

        private string pathImgFolder;
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


        // МЕТОДЫ

        /// <summary>
        /// Создание вкладки ленты, пустой, нужно добавить панель(панели), в напели добавить кнопки.
        /// </summary>
        /// <param name="ribbonTabTitle">Заголовок вкладки ленты</param>
        /// <param name="ribbonTabID">ID вкладки ленты</param>
        /// <returns>Ссылка на объект "Вкладка ленты"</returns>
        public RibbonTab GetRibTab(string ribbonTabTitle, string ribbonTabID)
        {
            try
            {
                if (GetIsRibbonTabLoadedRef(ribbonTabTitle, ribbonTabID) == null)
                // добавляем свою вкладку
                {
                    RibbonTab ribTab = new RibbonTab()
                    {
                        Title = ribbonTabTitle,
                        Id = ribbonTabID
                    };

                    return ribTab;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {
                // return null;
                throw;
            }
        }

        /// <summary>
        /// Создание Панели вкладки ленты, пустой, нужно еще добавить кнопки. Саму панель нужно вставить во Вкладку ribbonTab.Panels.Add(ribPanel).
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
        /// Создание кнопки
        /// </summary>
        /// <param name="ribButtonText">Текст кнопки</param>
        /// <param name="showText">Показать текст</param>
        /// <param name="showImage">Показать картинку</param>
        /// <param name="ribButtonLargeImageName">Большая картинка</param>
        /// <param name="ribButtonImageName">Стандартная картинка</param>
        /// <returns>Ссылка на объект кнопки</returns>
        public RibbonButton GetRibButton(string ribButtonText, bool showText,
            RibbonItemSize ribButtonSize, Orientation ribButtonOrientation,
            DelegateRibButtonHandler delegateRibBtnEv,
            bool showImage,
            string ribButtonLargeImageName = "image_large.png",
            string ribButtonImageName = "image_standart.png") // аргумент не обязательный

        {
            //// Для сообщений
            //AcadSendMess AcSM = new AcadSendMess();

            // Создадим объект кнопки.
            AdW.RibbonButton ribButton = new RibbonButton();

            try
            {
                // Полный путь к файлам картинок:
                string pathDirIm;

                // Проверим путь к папке img. 
                if (pathImgFolder == null || pathImgFolder == String.Empty) // если свойство PathImgFolder не задано.
                {
                    // Если никаких путей нет, то будем искать в папке <InstallDir>/img/ 
                    // Там должны быть файлы image_standart.png, image_large.png. Их мы и подключим.

                    // Зададим путь к папке, откуда запущена Dll.
                    DirectoryInfo dirDLL = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);

                    // Зададим полный путь к папке img:  <InstallDir>/img/ 
                    pathDirIm = $"{dirDLL.Parent.Parent.Parent.FullName}/img/";
                    //AcSM.SendStringDebugStars(new List<string> { "Путь к папке картинок", $"{pathDirIm}" });
                }
                else // если свойство PathImgFolder не нулевое, т.е. мы прописали сами путь к папке картинок, то
                {
                    pathDirIm = pathImgFolder; // присвоим тот путь, кот. задали сами
                }

                // Теперь займемся файлами картинок.
                if (Directory.Exists(pathDirIm)) // Проверим, что в <InstallDir> есть папка img.
                {
                    // получим путь к файлу большой картинки.
                    string pathLargeImg = pathDirIm + ribButtonLargeImageName;
                    // получим путь к файлу малой картинки.
                    string pathImg = pathDirIm + ribButtonImageName;

                    //AcSM.SendStringDebugStars(new List<string> { "Полный путь к папке картинок",
                    //    $"{pathDirIm}",
                    //    "Путь к большой картинке",
                    //    $"{pathLargeImg}",
                    //    "Путь к малой картинке",
                    //    $"{pathImg}"
                    //});

                    // Проверим существование картинок в папке IMG.
                    if (File.Exists(pathLargeImg) && File.Exists(pathImg))
                    {
                        // создадим объекты BitmapImage
                        BitmapImage bitmapImage32 = new BitmapImage(new Uri(pathLargeImg)); // путь к большой картинке
                        BitmapImage bitmapImage16 = new BitmapImage(new Uri(pathImg));// путь к большой картинке

                        ribButton.Image = bitmapImage16;
                        ribButton.LargeImage = bitmapImage32;
                        ribButton.ShowImage = showImage;
                    }
                    else // Если картинок для кнопки так и не найдено, отключим их прорисовку
                    {
                        ribButton.ShowImage = false;
                        //AcSM.SendStringDebugStars("Картинок для кнопок не найдено");
                    }
                }
                else // если какой-то сбой и пути к картинкам вообще нигде не найдено
                {
                    ribButton.ShowImage = false;
                    //AcSM.SendStringDebugStars("Что-то не так с картинками!");
                }

                // С картинками закончено, настроим др. свойства кнопки.
                ribButton.Text = ribButtonText;
                ribButton.ShowText = showText;
                ribButton.Size = ribButtonSize;
                ribButton.Orientation = ribButtonOrientation;
                ribButton.CommandHandler = new RibBtnHdlrDel(delegateRibBtnEv);

                return ribButton;
            }

            #region CATCH
            catch (System.Exception)
            {

                //throw;
                //AcSM.SendStringDebugStars(new List<string>
                //{$"Создание кнопки {ribButtonText} прервано", "Класс:", $"{this}", "Метод:", "GetRibButton"});

                return null;
            }
            #endregion
        }

        /*
        // Проверка "загруженности" ленты
        public bool GetIsRibbonLoaded(string ribbonTabTitle, string ribbonTabID)
        {
            bool ribLoaded = false;
            //RibbonControl ribCntrl = Autodesk.Windows.ComponentManager.Ribbon;
            // Делаем итерацию по вкладкам ленты
            foreach (RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                // И если у вкладки совпадает идентификатор и заголовок, то значит вкладка загружена
                if (tab.Id.Equals(ribbonTabID) & tab.Title.Equals(ribbonTabTitle))
                {
                    ribLoaded = true;
                    break;
                }
                else ribLoaded = false;
            }
            return ribLoaded;
        } */

        /// <summary>
        /// Проверка существования вклвдки на ленте
        /// </summary>
        /// <param name="ribbonTabTitle">Название вкладки</param>
        /// <param name="ribbonTabID">ID вкладки</param>
        /// <returns>вкладка, тип RibbonTab, если найдена в ленте. Иначе - null</returns>
        public RibbonTab GetIsRibbonTabLoadedRef(string ribbonTabTitle, string ribbonTabID)
        {
            RibbonTab rbTab = null;
            //RibbonControl ribCntrl = Autodesk.Windows.ComponentManager.Ribbon;
            // Делаем итерацию по вкладкам ленты
            foreach (RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                // И если у вкладки совпадает идентификатор и заголовок, то значит вкладка загружена
                if (tab.Id.Equals(ribbonTabID) & tab.Title.Equals(ribbonTabTitle))
                {
                    // return tab;
                    rbTab = tab;
                    // break;
                }
            }
            return rbTab; // вернем ее
        }

        /// <summary>
        /// Проверяет сущ. панели во вкладке.
        /// </summary>
        /// <param name="ribbonTab">Объект вкладки</param>
        /// <param name="ribbonPanelTitle">Заголовок панели</param>
        /// <returns>Объект Панели, если такая содержится в проверяемой вкладке. NULL - если такой панели нет.</returns>
        public RibbonPanel GetIsRibbonPanelLoadedRef(RibbonTab ribbonTab, string ribbonPanelTitle)
        {
            RibbonPanel RibPan = null; // = new RibbonPanel();

            // проверим вкладку, если она вообще не загружена, то и смысла нет проверять панель
            if (GetIsRibbonTabLoadedRef(ribbonTab.Title, ribbonTab.Id) != null) // если вкладка загружена в ленту
            {
                // Делаем итерацию по панелям вклвдки
                foreach (RibbonPanel pan in ribbonTab.Panels)
                {
                    if (pan.Source.Title.Equals(ribbonPanelTitle)) // если такая панель есть на сущ. вкладке.
                    {
                        RibPan = pan;
                        //break;
                    }
                }
            }

            return RibPan; // Если панели нет, возращ. null
        }

        public RibbonButton GetIsRibbonButtonLoadedRef(RibbonTab ribbonTab, RibbonPanel ribbonPanel, string ribbonButtonText)
        {
            RibbonButton RibBtn = null;
            // проверим панель, если она вообще не загружена, то и смысла нет проверять сущ. кнопки
            if (GetIsRibbonPanelLoadedRef(ribbonTab, ribbonPanel.Source.Title) != null)
            {
                foreach (RibbonButton rRtn in ribbonPanel.Source.Items) // проверим кнопки панели
                {
                    if (rRtn.Text.Equals(ribbonButtonText)) // если текст кнопки совпал, вернем эту кнопку.
                    {
                        RibBtn = rRtn;
                    }
                }
            }
            return RibBtn;
        }

    }

    /// <summary>
    /// Быстрое создание вкладки ленты
    /// </summary>
    public class CreateRibTabSpeed
    {
        // КОНСТРУКТОРЫ

        /*
        /// <summary>
        /// Создание вкладки ленты с одной панелью, кнопки одинаковые, в ряд.
        /// </summary>
        /// <param name="ribbonTabTitle">Заголовок вкладки</param>
        /// <param name="ribbonTabID">ID вкладки</param>
        /// <param name="listRibbonButtons">список из кнопок, заданных структурой RibButtonMyShort </param>
        internal CreateRibTabSpeed(string ribbonTabTitle, string ribbonTabID, List<RibButtonMyShort> listRibbonButtons)
        {
            ribTabTitle = ribbonTabTitle; ribTabID = ribbonTabID; listRibButtons = listRibbonButtons;
        }
        */

        /*
        /// <summary>
        /// Создание вкладки ленты, добавление панели в сущ. вклвдку.
        /// </summary>
        /// <param name="ribbonTabTitle">Заголовок вкладки</param>
        /// <param name="ribbonTabID">ID вкладки</param>
        /// <param name="listRibbonButtons">список из кнопок, заданных структурой RibButtonMyFull </param>
        CreateRibTabSpeed(string ribbonTabTitle, string ribbonTabID, List<RibButtonMyFull> listRibbonButtons) { }
        */

        // ПОЛЯ

        /*
        private string ribTabTitle;
        private string ribTabID;
        private List<RibButtonMyShort> listRibButtons;
        */

        // создадим класс по работе с вкладками
        RibbonCreateEasy RibCr = new RibbonCreateEasy();


        // МЕТОДЫ

        internal void CreateOrModifityRibbonTab(string ribbonTabTitle, string ribbonTabID, string ribbonPanelTitle, List<RibButtonMyFull> listRibbonButtons, bool modifityPanel = false)
        {
            // проверим вкладку на существование в ленте
            RibbonTab RibTab = RibCr.GetIsRibbonTabLoadedRef(ribbonTabTitle, ribbonTabID);

            // если такой вкладки нет, 
            if (RibTab == null)
            {
                RibTab = RibCr.GetRibTab(ribbonTabTitle, ribbonTabID); //создадим вкладку
            } // иначе работаем с существующей.

            //  создадим/модифицируем панель.
            CreateOrModifityRibbonPanel(RibTab, ribbonPanelTitle, listRibbonButtons, modifityPanel);

        }

        internal void CreateOrModifityRibbonPanel(RibbonTab ribbonTab, string ribbonPanelTitle, List<RibButtonMyFull> listRibbonButtons, bool modifityPanel)
        {
            //  проверим панель на существование во вкладке. 
            RibbonPanel ribbonPan = RibCr.GetIsRibbonPanelLoadedRef(ribbonTab, ribbonPanelTitle);

            // если такой панели нет, создадим
            if (ribbonPan == null)
            {
                ribbonPan = RibCr.GetRibPanel(ribbonPanelTitle); //создадим панель
            }// иначе работаем с существующей.

            // передаем панель дальше, создаем кнопки
            //если установить  modifityPanel = true, то можно добавить кнопки в сущ. панель.
            CreateOrModifityRibbonButton(ribbonTab, ribbonPan, listRibbonButtons, modifityPanel);
        }

        internal void CreateOrModifityRibbonButton(RibbonTab ribbonTab, RibbonPanel ribbonPan, List<RibButtonMyFull> listRibbonButtons, bool modifityPanel)
        {
            bool isPanelExist; // если вкладка  существует

            // если вкладка  существует
            if (RibCr.GetIsRibbonPanelLoadedRef(ribbonTab, ribbonPan.Source.Title) != null) // панель есть
            {
                isPanelExist = true;
            }
            else { isPanelExist = false; }


            // если вкладка и панель существует и modifityPanel = true   ИЛИ   панели не существует
            if ((isPanelExist && modifityPanel) || (!isPanelExist))
            {

                foreach (RibButtonMyFull RbBtn in listRibbonButtons)
                {
                    // проверим, есть ли такая кнопка
                    if (RibCr.GetIsRibbonButtonLoadedRef(ribbonTab, ribbonPan, RbBtn.ribButtonText) == null)
                    {
                        // если кнопки нет, добавим ее.
                        ribbonPan.Source.Items.Add(
                            RibCr.GetRibButton(
                            RbBtn.ribButtonText, RbBtn.showText,
                            RbBtn.ribButtonSize, RbBtn.ribButtonOrientation,
                            RbBtn.delegateRibBtnEv,
                            RbBtn.showImage,
                            RbBtn.ribButtonLargeImageName, RbBtn.ribButtonImageName
                            ));
                    }
                }
            }

            //  если панели нет 
            if (!isPanelExist)
            {
                // Вставим панель во вкладку.
                ribbonTab.Panels.Add(ribbonPan);
                // добавляем вкладку в ленту, если такой еще не загружено
                if (RibCr.GetIsRibbonTabLoadedRef(ribbonTab.Title, ribbonTab.Id) == null)
                {
                    Autodesk.Windows.ComponentManager.Ribbon.Tabs.Add(ribbonTab);
                }
            }

            // если панель есть, она уже загружена и в коде выше уже добавлены кнопки.


            // Обновим ленту.
            Autodesk.Windows.ComponentManager.Ribbon.UpdateLayout();
        }

    }



    // -----------------------------------------------------------------------------------------------
    /// СТРУКТУРЫ

    /// <summary>
    ///  Кнопка ленты: Текст кнопки. Показать текст. Размер кнопки. Ориентация кнопки. Показать картинку. Имя файла большой картинки. Имя файла малой картинки. Экземпляр делегата
    /// </summary>
    internal struct RibButtonMyFull
    {
        /// <value>
        ///  Текст кнопки.
        /// </value>
        internal string ribButtonText;
        /// <value>
        ///  Показать текст.
        /// </value>
        internal bool showText;
        /// <value>
        ///  Размер кнопки.
        /// </value>
        internal RibbonItemSize ribButtonSize;
        /// <value>
        ///  Ориентация кнопки.
        /// </value>
        internal Orientation ribButtonOrientation;
        /// <value>
        ///  Показать картинку.
        /// </value>
        internal bool showImage;
        /// <value>
        ///  Имя файла большой картинки.
        /// </value>
        internal string ribButtonLargeImageName;
        /// <value>
        ///  Имя файла малой картинки.
        /// </value>
        internal string ribButtonImageName;
        /// <value>
        ///  Экземпляр делегата, кот. содержит методы-обработчики нажатия кнопки.
        /// </value>
        internal DelegateRibButtonHandler delegateRibBtnEv;
    }

    /// <summary>
    /// Кнопка ленты: Текст, Размер.
    /// </summary>
    internal struct RibButtonMyShort
    {
        /// <value>
        /// Текст кнопки.
        /// </value>
        internal string ribButtonText;
        //bool showText;
        /// <value>
        /// Размер кнопки..
        /// </value>
        internal RibbonItemSize ribButtonSize;
        //Orientation ribButtonOrientation;
        //bool showImage;
        //string ribButtonLargeImageName;
        //string ribButtonImageName;

        internal DelegateRibButtonHandler delegateRibBtnEv;
    }


}
