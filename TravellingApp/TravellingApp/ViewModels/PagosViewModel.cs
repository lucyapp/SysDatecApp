using System.Collections.Generic;
using System.Collections.ObjectModel;
using SysDatecScanApp.Models;
using SysDatecScanApp.Services;
using Xamarin.Forms;

namespace SysDatecScanApp.ViewModels
{
    public class PagosViewModel : BindableObject
    {
        private ObservableCollection<Post> _posts;
        private Post _currentPost;
        private List<Post> posts;
        
        public PagosViewModel()
        {
           
                LoadPosts(); 

        }

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        public Post CurrentPost
        {
            get { return _currentPost; }
            set
            {
                _currentPost = value;
                OnPropertyChanged();
            }
        }

        private void LoadPosts()
        {
             //posts = MockPostService.Instance.GetCommunityPosts();
             //Posts = new ObservableCollection<Post>(posts);
             //CurrentPost = Posts[0];
        }
    }
}
