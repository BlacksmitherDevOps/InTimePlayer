﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InTime.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Song_Singer", Namespace="http://schemas.datacontract.org/2004/07/PlayerService")]
    [System.SerializableAttribute()]
    public partial class Song_Singer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private InTime.ServiceReference1.Singer_Album[] AlbumsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public InTime.ServiceReference1.Singer_Album[] Albums {
            get {
                return this.AlbumsField;
            }
            set {
                if ((object.ReferenceEquals(this.AlbumsField, value) != true)) {
                    this.AlbumsField = value;
                    this.RaisePropertyChanged("Albums");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Singer_Album", Namespace="http://schemas.datacontract.org/2004/07/PlayerService")]
    [System.SerializableAttribute()]
    public partial class Singer_Album : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImagePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private InTime.ServiceReference1.Song_Singer SingerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private InTime.ServiceReference1.Song[] SongsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImagePath {
            get {
                return this.ImagePathField;
            }
            set {
                if ((object.ReferenceEquals(this.ImagePathField, value) != true)) {
                    this.ImagePathField = value;
                    this.RaisePropertyChanged("ImagePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public InTime.ServiceReference1.Song_Singer Singer {
            get {
                return this.SingerField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerField, value) != true)) {
                    this.SingerField = value;
                    this.RaisePropertyChanged("Singer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public InTime.ServiceReference1.Song[] Songs {
            get {
                return this.SongsField;
            }
            set {
                if ((object.ReferenceEquals(this.SongsField, value) != true)) {
                    this.SongsField = value;
                    this.RaisePropertyChanged("Songs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Song", Namespace="http://schemas.datacontract.org/2004/07/PlayerService")]
    [System.SerializableAttribute()]
    public partial class Song : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Album_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] MusicField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private InTime.ServiceReference1.Song_Singer[] SingersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalListensField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool VerificationField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Album_ID {
            get {
                return this.Album_IDField;
            }
            set {
                if ((this.Album_IDField.Equals(value) != true)) {
                    this.Album_IDField = value;
                    this.RaisePropertyChanged("Album_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Genre {
            get {
                return this.GenreField;
            }
            set {
                if ((object.ReferenceEquals(this.GenreField, value) != true)) {
                    this.GenreField = value;
                    this.RaisePropertyChanged("Genre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Music {
            get {
                return this.MusicField;
            }
            set {
                if ((object.ReferenceEquals(this.MusicField, value) != true)) {
                    this.MusicField = value;
                    this.RaisePropertyChanged("Music");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Path {
            get {
                return this.PathField;
            }
            set {
                if ((this.PathField.Equals(value) != true)) {
                    this.PathField = value;
                    this.RaisePropertyChanged("Path");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public InTime.ServiceReference1.Song_Singer[] Singers {
            get {
                return this.SingersField;
            }
            set {
                if ((object.ReferenceEquals(this.SingersField, value) != true)) {
                    this.SingersField = value;
                    this.RaisePropertyChanged("Singers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalListens {
            get {
                return this.TotalListensField;
            }
            set {
                if ((this.TotalListensField.Equals(value) != true)) {
                    this.TotalListensField = value;
                    this.RaisePropertyChanged("TotalListens");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Verification {
            get {
                return this.VerificationField;
            }
            set {
                if ((this.VerificationField.Equals(value) != true)) {
                    this.VerificationField = value;
                    this.RaisePropertyChanged("Verification");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewSinger", ReplyAction="http://tempuri.org/IService1/AddNewSingerResponse")]
        void AddNewSinger(InTime.ServiceReference1.Song_Singer NewSinger);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewSinger", ReplyAction="http://tempuri.org/IService1/AddNewSingerResponse")]
        System.Threading.Tasks.Task AddNewSingerAsync(InTime.ServiceReference1.Song_Singer NewSinger);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewTrack", ReplyAction="http://tempuri.org/IService1/AddNewTrackResponse")]
        void AddNewTrack(InTime.ServiceReference1.Song NewSong);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewTrack", ReplyAction="http://tempuri.org/IService1/AddNewTrackResponse")]
        System.Threading.Tasks.Task AddNewTrackAsync(InTime.ServiceReference1.Song NewSong);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAlbum", ReplyAction="http://tempuri.org/IService1/GetAlbumResponse")]
        InTime.ServiceReference1.Singer_Album GetAlbum(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAlbum", ReplyAction="http://tempuri.org/IService1/GetAlbumResponse")]
        System.Threading.Tasks.Task<InTime.ServiceReference1.Singer_Album> GetAlbumAsync(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/TempAlbum", ReplyAction="http://tempuri.org/IService1/TempAlbumResponse")]
        InTime.ServiceReference1.Singer_Album TempAlbum();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/TempAlbum", ReplyAction="http://tempuri.org/IService1/TempAlbumResponse")]
        System.Threading.Tasks.Task<InTime.ServiceReference1.Singer_Album> TempAlbumAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrackStream", ReplyAction="http://tempuri.org/IService1/GetTrackStreamResponse")]
        System.IO.Stream GetTrackStream(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrackStream", ReplyAction="http://tempuri.org/IService1/GetTrackStreamResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GetTrackStreamAsync(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewAlbum", ReplyAction="http://tempuri.org/IService1/AddNewAlbumResponse")]
        void AddNewAlbum(InTime.ServiceReference1.Singer_Album NewAlbum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddNewAlbum", ReplyAction="http://tempuri.org/IService1/AddNewAlbumResponse")]
        System.Threading.Tasks.Task AddNewAlbumAsync(InTime.ServiceReference1.Singer_Album NewAlbum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFile", ReplyAction="http://tempuri.org/IService1/GetFileResponse")]
        byte[] GetFile();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFile", ReplyAction="http://tempuri.org/IService1/GetFileResponse")]
        System.Threading.Tasks.Task<byte[]> GetFileAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DownloadFile", ReplyAction="http://tempuri.org/IService1/DownloadFileResponse")]
        void DownloadFile(byte[] arr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DownloadFile", ReplyAction="http://tempuri.org/IService1/DownloadFileResponse")]
        System.Threading.Tasks.Task DownloadFileAsync(byte[] arr);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : InTime.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<InTime.ServiceReference1.IService1>, InTime.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddNewSinger(InTime.ServiceReference1.Song_Singer NewSinger) {
            base.Channel.AddNewSinger(NewSinger);
        }
        
        public System.Threading.Tasks.Task AddNewSingerAsync(InTime.ServiceReference1.Song_Singer NewSinger) {
            return base.Channel.AddNewSingerAsync(NewSinger);
        }
        
        public void AddNewTrack(InTime.ServiceReference1.Song NewSong) {
            base.Channel.AddNewTrack(NewSong);
        }
        
        public System.Threading.Tasks.Task AddNewTrackAsync(InTime.ServiceReference1.Song NewSong) {
            return base.Channel.AddNewTrackAsync(NewSong);
        }
        
        public InTime.ServiceReference1.Singer_Album GetAlbum(int ID) {
            return base.Channel.GetAlbum(ID);
        }
        
        public System.Threading.Tasks.Task<InTime.ServiceReference1.Singer_Album> GetAlbumAsync(int ID) {
            return base.Channel.GetAlbumAsync(ID);
        }
        
        public InTime.ServiceReference1.Singer_Album TempAlbum() {
            return base.Channel.TempAlbum();
        }
        
        public System.Threading.Tasks.Task<InTime.ServiceReference1.Singer_Album> TempAlbumAsync() {
            return base.Channel.TempAlbumAsync();
        }
        
        public System.IO.Stream GetTrackStream(int ID) {
            return base.Channel.GetTrackStream(ID);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GetTrackStreamAsync(int ID) {
            return base.Channel.GetTrackStreamAsync(ID);
        }
        
        public void AddNewAlbum(InTime.ServiceReference1.Singer_Album NewAlbum) {
            base.Channel.AddNewAlbum(NewAlbum);
        }
        
        public System.Threading.Tasks.Task AddNewAlbumAsync(InTime.ServiceReference1.Singer_Album NewAlbum) {
            return base.Channel.AddNewAlbumAsync(NewAlbum);
        }
        
        public byte[] GetFile() {
            return base.Channel.GetFile();
        }
        
        public System.Threading.Tasks.Task<byte[]> GetFileAsync() {
            return base.Channel.GetFileAsync();
        }
        
        public void DownloadFile(byte[] arr) {
            base.Channel.DownloadFile(arr);
        }
        
        public System.Threading.Tasks.Task DownloadFileAsync(byte[] arr) {
            return base.Channel.DownloadFileAsync(arr);
        }
    }
}
