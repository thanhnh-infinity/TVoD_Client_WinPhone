﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using iTVOD_WindowPhone7.TVOD.TVODClass;
using Newtonsoft.Json;
using System.IO;
using System.IO.IsolatedStorage;
using iTVOD_WindowPhone7.TVOD.System;
using Microsoft.Phone.Shell;
using System.Windows.Data;


namespace iTVOD_WindowPhone7
{
    public partial class SearchActivity : PhoneApplicationPage
    {
        private Ultility tvodUltility;

        /** Dieu khien scoll View cua ung dung **/
        ScrollViewer scrollViewer;

        /** Tham so chuyen dong cua ung dung **/
        private bool fixCacheImage = false;

        /** Properties **/
        ObservableCollection<VideoClass> ds= new ObservableCollection<VideoClass>();
        private List<VideoClass> listVideo = new List<VideoClass>();
        private VideoClass videoObj = new VideoClass();
        private int numberVideo = 0;
        private int numberVideoPerPage = 0;

        //private string folder = "TVOD";
        //private String childCategoryFile = "";
        private string responseResult = "";
        private ImageCache imgCache;

        /** Properties get from previous activity **/
        private int current_page = 1;
        private int total_video = 0;
        private string keyword = "";

        private string cacheKeywordFile = "tvod_keyword.txt";
        //private string ImageFileName = null;
        private string folder = "TVOD";
        /** Progress Bar Indicator **/
        ProgressIndicator prog;

        public SearchActivity()
        {
            InitializeComponent();

            setUpApplicationBar();

            txtKeyWord.Focus();
            disableProgressBar();
            imgCache = new ImageCache();
            this.lstVideoSearching.Loaded += new RoutedEventHandler(lstVideoSearching_Loaded);
        }

        public void setUpApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = true;

            ApplicationBarIconButton btnBack = new ApplicationBarIconButton(new Uri("/icon/back.png", UriKind.Relative));
            btnBack.Text = "Back";
            ApplicationBarIconButton btnHome = new ApplicationBarIconButton(new Uri("/icon/home.png", UriKind.Relative));
            btnHome.Text = "Home";
            ApplicationBarIconButton btnUserProfile = new ApplicationBarIconButton(new Uri("/icon/user_profile.png", UriKind.Relative));
            btnUserProfile.Text = "Người dùng";

            ApplicationBar.Buttons.Add(btnBack);
            ApplicationBar.Buttons.Add(btnHome);
            ApplicationBar.Buttons.Add(btnUserProfile);
        
            btnHome.Click += new EventHandler(btnHome_Click);
            btnUserProfile.Click += new EventHandler(btnUserProfile_Click);
            btnBack.Click  += new EventHandler(btnBack_Click);

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnUserProfile_Click(object sender, EventArgs e)
        {
            try
            {
                tvodUltility = new Ultility();
                bool isLogin = tvodUltility.checkLogin();
                if (isLogin == true)
                {

                    NavigationService.Navigate(new Uri("/UserProfileActivity_V2.xaml", UriKind.Relative));
                }
                else
                {
                    (App.Current as App).navigation_tvod = "MAIN_PAGE";
                    LoginActivity_V2 loginWindow = new LoginActivity_V2();
                    loginWindow.Show();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            (App.Current as App).loadData = false;
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if ((App.Current as App).loadData == true)
            {
                this.txtKeyWord.Focus();
                txtKeyWord.Text = getCacheKeyword(this.folder, this.cacheKeywordFile);
                base.OnNavigatedTo(e);
            }
            else
            {
                (App.Current as App).loadData = true;
                base.OnNavigatedTo(e);
            }
        }

        private string getCacheKeyword(string folder, string cacheKeywordFile)
        {
            try
            {
                IsolatedStorageFile File = IsolatedStorageFile.GetUserStoreForApplication();
                string sFile = folder + "\\" + cacheKeywordFile;
                bool fileExist = File.FileExists(sFile);
                string rawData = "";
                if (fileExist == true)
                {
                    StreamReader reader = new StreamReader(new IsolatedStorageFileStream(sFile, FileMode.Open, File));
                    rawData = reader.ReadToEnd();
                    reader.Close();
                    return rawData;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return "";
            }
        }

        public void BindingDataVideo_ServerData_Searching(string keyword, string page)
        {
            try
            {
                string cmsURL = SystemParameter.REQUEST_SEARCHING;
                cmsURL = cmsURL.Replace("%p", page);
                cmsURL = cmsURL.Replace("%k", keyword);
                this.current_page = Int32.Parse(page);
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_searching_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(cmsURL));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void client_searching_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string data = e.Result;
                this.responseResult = data;

                parseJSONVideoList(this.responseResult);
                //ds = new ObservableCollection<VideoClass>();
                if (this.current_page == 1) 
                    ds.Clear();
                if (this.numberVideo > 0)
                {
                    for (int i = 0; i < numberVideoPerPage; i++)
                    {
                        videoObj = new VideoClass();
                        videoObj = listVideo[i];
                        //string source_image = imgCache.getImage(parentCategoryObj.category_image);
                        string source_image = "";
                        if (fixCacheImage == true)
                        {
                            //source_image = imgCache.getImage_2(childCategoryObj.category_id);
                        }
                        else //Day la hoan thien nhat tuy nhien can thoi gian de fix data ( cache Image)
                        {
                            source_image = imgCache.getImage(videoObj.video_picture_path);
                        }

                        ds.Add(new VideoClass() { video_picture_path = source_image, video_english_title = videoObj.video_english_title, video_vietnamese_title = videoObj.video_vietnamese_title, video_number_views = videoObj.video_number_views, video_id = videoObj.video_id, video_price = videoObj.video_price });
                    }
                    this.lstVideoSearching.ItemsSource = ds;
                    disableProgressBar();
                }
                else
                {
                    ds.Add(new VideoClass() { video_picture_path = "http", video_english_title = "Không có dữ liệu", video_vietnamese_title = "", video_number_views = "", video_id = "", video_price="0.00" });
                    this.lstVideoSearching.ItemsSource = ds;
                    disableProgressBar();
                }
                disableProgressIndicator();
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void enableProgressIndicator()
        {
            prog = new ProgressIndicator();
            prog.IsVisible = true;
            prog.IsIndeterminate = true;
            prog.Text = "Xin hãy đợi trong giây lát";
            btnSearch.IsEnabled = false;
            txtKeyWord.IsEnabled = false;
            SystemTray.SetProgressIndicator(this, prog);
        }

        public void disableProgressIndicator()
        {
            prog.IsVisible = false;
            btnSearch.IsEnabled = true;
            txtKeyWord.IsEnabled = true;
        }

        private void disableProgressBar()
        {
            btnSearch.IsEnabled = true;
            txtKeyWord.IsEnabled = true;
        }

        private void enableProgressBar()
        {
            if (ds == null)
            {
                ds = new ObservableCollection<VideoClass>();

            }
            ds.Add(new VideoClass() { video_picture_path = "http", video_english_title = "Loading.............", video_vietnamese_title = "", video_number_views = "", video_id = "", video_price="0.00" });
            this.lstVideoSearching.ItemsSource = ds;
            btnSearch.IsEnabled = false;
            txtKeyWord.IsEnabled = false;
        }

        private void parseJSONVideoList(String jsonVideoList)
        {
            try
            {
                var videoListJSONObj = JsonConvert.DeserializeObject<RootVideoClass>(jsonVideoList);
                if (videoListJSONObj.success == true || videoListJSONObj.type == "video")
                {
                    this.total_video = Int32.Parse(videoListJSONObj.total_quantity);
                    listVideo = new List<VideoClass>();
                    foreach (var obj in videoListJSONObj.items)
                    {
                        videoObj = new VideoClass();
                        videoObj.video_id = obj.video_id;
                        videoObj.video_description = obj.video_description;
                        videoObj.video_duration = obj.video_duration;
                        videoObj.video_english_title = obj.video_english_title;
                        videoObj.video_number_views = "Số lượt xem : " + obj.video_number_views;
                        videoObj.video_picture_path = obj.video_picture_path;
                        videoObj.video_price = obj.video_price;
                        videoObj.video_vietnamese_title = obj.video_vietnamese_title;
                        listVideo.Add(videoObj);

                    }
                    this.numberVideoPerPage = Int16.Parse(videoListJSONObj.quantity);
                    this.numberVideo = Int16.Parse(videoListJSONObj.total_quantity);
                }
                else
                {
                    videoObj = new VideoClass();
                    videoObj.video_english_title = "Không có dữ liệu !";
                    this.numberVideoPerPage = 0;
                    this.numberVideo = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.keyword = txtKeyWord.Text.Trim();
            enableProgressBar();
            saveKeywordToIsolatedStorage(this.folder, this.cacheKeywordFile, this.keyword);
            BindingDataVideo_ServerData_Searching(this.keyword, "1");
        }

        public bool saveKeywordToIsolatedStorage(string folder, string file, string keyword)
        {
            try
            {
                IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication();

                if (!ISF.DirectoryExists(folder))
                {
                    ISF.CreateDirectory(folder);
                }
                using (StreamWriter SW = new StreamWriter(new IsolatedStorageFileStream(folder + "\\" + file, FileMode.Create, FileAccess.Write, ISF)))
                {
                    SW.WriteLine(keyword);
                    SW.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private void lstVideoSearching_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                VideoClass videoObjTemp = (sender as ListBox).SelectedItem as VideoClass;
                NavigationService.Navigate(new Uri("/Video_Detail_Player_V2.xaml?video_id=" + videoObjTemp.video_id + "&video_english_title=" + videoObjTemp.video_english_title + "&video_vietnamese_title=" + videoObjTemp.video_vietnamese_title + "&video_picture_path=" + videoObjTemp.video_picture_path + "&video_price=" + videoObjTemp.video_price, UriKind.Relative));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        /**************************************************************************************************/
        /** Giai quyet van de phan trang **/

        void lstVideoSearching_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = (FrameworkElement)sender;
                element.Loaded -= lstVideoSearching_Loaded;
                scrollViewer = FindChildOfType<ScrollViewer>(element);
                if (scrollViewer == null)
                {
                    throw new InvalidOperationException("ScrollViewer not found.");
                }

                Binding binding = new Binding();
                binding.Source = scrollViewer;
                binding.Path = new PropertyPath("VerticalOffset");
                binding.Mode = BindingMode.OneWay;
                this.SetBinding(ListVerticalOffsetProperty, binding);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static void OnListVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SearchActivity page = obj as SearchActivity;
            ScrollViewer viewer = page.scrollViewer;

            //Checks if the Scroll has reached the last item based on the ScrollableHeight
            bool atBottom = viewer.VerticalOffset >= viewer.ScrollableHeight;

            if (atBottom)
            {
                page.enableProgressIndicator();
                page.BindingDataVideo_ServerData_Searching(page.keyword, (page.current_page + 1).ToString());
                //BindingDataVideo_ServerData_Paging("newest", "2");
            }
        }

        public readonly DependencyProperty ListVerticalOffsetProperty = DependencyProperty.Register("ListVerticalOffset", typeof(double), typeof(SearchActivity),
           new PropertyMetadata(new PropertyChangedCallback(OnListVerticalOffsetChanged)));

        public double ListVerticalOffset
        {
            get { return (double)this.GetValue(ListVerticalOffsetProperty); }
            set { this.SetValue(ListVerticalOffsetProperty, value); }
        }

        /// <summary>
        /// Finding the ScrollViewer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }


}