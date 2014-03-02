﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncPiwikToMSSQL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="offLineBBHome")]
	public partial class offlineBbhomeDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertGA_Log(GA_Log instance);
    partial void UpdateGA_Log(GA_Log instance);
    partial void DeleteGA_Log(GA_Log instance);
    partial void InsertPiwik_CustomerAction(Piwik_CustomerAction instance);
    partial void UpdatePiwik_CustomerAction(Piwik_CustomerAction instance);
    partial void DeletePiwik_CustomerAction(Piwik_CustomerAction instance);
    partial void InsertPiwik_log(Piwik_log instance);
    partial void UpdatePiwik_log(Piwik_log instance);
    partial void DeletePiwik_log(Piwik_log instance);
    partial void Insertpiwik_log_reffer(piwik_log_reffer instance);
    partial void Updatepiwik_log_reffer(piwik_log_reffer instance);
    partial void Deletepiwik_log_reffer(piwik_log_reffer instance);
    #endregion
		
		public offlineBbhomeDataContext() : 
				base(global::SyncPiwikToMSSQL.Properties.Settings.Default.offLineBBHomeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public offlineBbhomeDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public offlineBbhomeDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public offlineBbhomeDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public offlineBbhomeDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Ga_guidUserID> Ga_guidUserIDs
		{
			get
			{
				return this.GetTable<Ga_guidUserID>();
			}
		}
		
		public System.Data.Linq.Table<GA_Log> GA_Logs
		{
			get
			{
				return this.GetTable<GA_Log>();
			}
		}
		
		public System.Data.Linq.Table<Piwik_CustomerAction> Piwik_CustomerActions
		{
			get
			{
				return this.GetTable<Piwik_CustomerAction>();
			}
		}
		
		public System.Data.Linq.Table<Piwik_log> Piwik_logs
		{
			get
			{
				return this.GetTable<Piwik_log>();
			}
		}
		
		public System.Data.Linq.Table<piwik_log_reffer> piwik_log_reffers
		{
			get
			{
				return this.GetTable<piwik_log_reffer>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Ga_guidUserID")]
	public partial class Ga_guidUserID
	{
		
		private int _uid;
		
		private string _guid;
		
		public Ga_guidUserID()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_uid", DbType="Int NOT NULL")]
		public int uid
		{
			get
			{
				return this._uid;
			}
			set
			{
				if ((this._uid != value))
				{
					this._uid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guid", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((this._guid != value))
				{
					this._guid = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GA_Log")]
	public partial class GA_Log : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.DateTime _GAStartDate;
		
		private System.DateTime _GAEndDate;
		
		private int _Type;
		
		private bool _Status;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnGAStartDateChanging(System.DateTime value);
    partial void OnGAStartDateChanged();
    partial void OnGAEndDateChanging(System.DateTime value);
    partial void OnGAEndDateChanged();
    partial void OnTypeChanging(int value);
    partial void OnTypeChanged();
    partial void OnStatusChanging(bool value);
    partial void OnStatusChanged();
    #endregion
		
		public GA_Log()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GAStartDate", DbType="DateTime NOT NULL")]
		public System.DateTime GAStartDate
		{
			get
			{
				return this._GAStartDate;
			}
			set
			{
				if ((this._GAStartDate != value))
				{
					this.OnGAStartDateChanging(value);
					this.SendPropertyChanging();
					this._GAStartDate = value;
					this.SendPropertyChanged("GAStartDate");
					this.OnGAStartDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GAEndDate", DbType="DateTime NOT NULL")]
		public System.DateTime GAEndDate
		{
			get
			{
				return this._GAEndDate;
			}
			set
			{
				if ((this._GAEndDate != value))
				{
					this.OnGAEndDateChanging(value);
					this.SendPropertyChanging();
					this._GAEndDate = value;
					this.SendPropertyChanged("GAEndDate");
					this.OnGAEndDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int NOT NULL")]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Bit NOT NULL")]
		public bool Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Piwik_CustomerAction")]
	public partial class Piwik_CustomerAction : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _label;
		
		private int _nb_actions;
		
		private int _idsubdatatable;
		
		private System.DateTime _dt;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnlabelChanging(string value);
    partial void OnlabelChanged();
    partial void Onnb_actionsChanging(int value);
    partial void Onnb_actionsChanged();
    partial void OnidsubdatatableChanging(int value);
    partial void OnidsubdatatableChanged();
    partial void OndtChanging(System.DateTime value);
    partial void OndtChanged();
    #endregion
		
		public Piwik_CustomerAction()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_label", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string label
		{
			get
			{
				return this._label;
			}
			set
			{
				if ((this._label != value))
				{
					this.OnlabelChanging(value);
					this.SendPropertyChanging();
					this._label = value;
					this.SendPropertyChanged("label");
					this.OnlabelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nb_actions", DbType="Int NOT NULL")]
		public int nb_actions
		{
			get
			{
				return this._nb_actions;
			}
			set
			{
				if ((this._nb_actions != value))
				{
					this.Onnb_actionsChanging(value);
					this.SendPropertyChanging();
					this._nb_actions = value;
					this.SendPropertyChanged("nb_actions");
					this.Onnb_actionsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idsubdatatable", DbType="Int NOT NULL")]
		public int idsubdatatable
		{
			get
			{
				return this._idsubdatatable;
			}
			set
			{
				if ((this._idsubdatatable != value))
				{
					this.OnidsubdatatableChanging(value);
					this.SendPropertyChanging();
					this._idsubdatatable = value;
					this.SendPropertyChanged("idsubdatatable");
					this.OnidsubdatatableChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dt", DbType="DateTime NOT NULL")]
		public System.DateTime dt
		{
			get
			{
				return this._dt;
			}
			set
			{
				if ((this._dt != value))
				{
					this.OndtChanging(value);
					this.SendPropertyChanging();
					this._dt = value;
					this.SendPropertyChanged("dt");
					this.OndtChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Piwik_log")]
	public partial class Piwik_log : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _userid;
		
		private string _guid;
		
		private string _url;
		
		private System.Nullable<System.DateTime> _lastVisitTime;
		
		private int _id;
		
		private string _action;
		
		private string _pagetitle;
		
		private System.Nullable<int> _spenttime;
		
		private string _refferurl;
		
		private string _keyword;
		
		private string _event_action;
		
		private string _visitIp;
		
		private string _location;
		
		private string _locationsina;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnuseridChanging(int value);
    partial void OnuseridChanged();
    partial void OnguidChanging(string value);
    partial void OnguidChanged();
    partial void OnurlChanging(string value);
    partial void OnurlChanged();
    partial void OnlastVisitTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnlastVisitTimeChanged();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnactionChanging(string value);
    partial void OnactionChanged();
    partial void OnpagetitleChanging(string value);
    partial void OnpagetitleChanged();
    partial void OnspenttimeChanging(System.Nullable<int> value);
    partial void OnspenttimeChanged();
    partial void OnrefferurlChanging(string value);
    partial void OnrefferurlChanged();
    partial void OnkeywordChanging(string value);
    partial void OnkeywordChanged();
    partial void Onevent_actionChanging(string value);
    partial void Onevent_actionChanged();
    partial void OnvisitIpChanging(string value);
    partial void OnvisitIpChanged();
    partial void OnlocationChanging(string value);
    partial void OnlocationChanged();
    partial void OnlocationsinaChanging(string value);
    partial void OnlocationsinaChanged();
    #endregion
		
		public Piwik_log()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userid", DbType="Int NOT NULL")]
		public int userid
		{
			get
			{
				return this._userid;
			}
			set
			{
				if ((this._userid != value))
				{
					this.OnuseridChanging(value);
					this.SendPropertyChanging();
					this._userid = value;
					this.SendPropertyChanged("userid");
					this.OnuseridChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guid", DbType="VarChar(50)")]
		public string guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((this._guid != value))
				{
					this.OnguidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("guid");
					this.OnguidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_url", DbType="VarChar(2000)")]
		public string url
		{
			get
			{
				return this._url;
			}
			set
			{
				if ((this._url != value))
				{
					this.OnurlChanging(value);
					this.SendPropertyChanging();
					this._url = value;
					this.SendPropertyChanged("url");
					this.OnurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastVisitTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> lastVisitTime
		{
			get
			{
				return this._lastVisitTime;
			}
			set
			{
				if ((this._lastVisitTime != value))
				{
					this.OnlastVisitTimeChanging(value);
					this.SendPropertyChanging();
					this._lastVisitTime = value;
					this.SendPropertyChanged("lastVisitTime");
					this.OnlastVisitTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_action", DbType="VarChar(50)")]
		public string action
		{
			get
			{
				return this._action;
			}
			set
			{
				if ((this._action != value))
				{
					this.OnactionChanging(value);
					this.SendPropertyChanging();
					this._action = value;
					this.SendPropertyChanged("action");
					this.OnactionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pagetitle", DbType="VarChar(500)")]
		public string pagetitle
		{
			get
			{
				return this._pagetitle;
			}
			set
			{
				if ((this._pagetitle != value))
				{
					this.OnpagetitleChanging(value);
					this.SendPropertyChanging();
					this._pagetitle = value;
					this.SendPropertyChanged("pagetitle");
					this.OnpagetitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_spenttime", DbType="Int")]
		public System.Nullable<int> spenttime
		{
			get
			{
				return this._spenttime;
			}
			set
			{
				if ((this._spenttime != value))
				{
					this.OnspenttimeChanging(value);
					this.SendPropertyChanging();
					this._spenttime = value;
					this.SendPropertyChanged("spenttime");
					this.OnspenttimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_refferurl", DbType="VarChar(2000)")]
		public string refferurl
		{
			get
			{
				return this._refferurl;
			}
			set
			{
				if ((this._refferurl != value))
				{
					this.OnrefferurlChanging(value);
					this.SendPropertyChanging();
					this._refferurl = value;
					this.SendPropertyChanged("refferurl");
					this.OnrefferurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_keyword", DbType="VarChar(2000)")]
		public string keyword
		{
			get
			{
				return this._keyword;
			}
			set
			{
				if ((this._keyword != value))
				{
					this.OnkeywordChanging(value);
					this.SendPropertyChanging();
					this._keyword = value;
					this.SendPropertyChanged("keyword");
					this.OnkeywordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_event_action", DbType="VarChar(1000)")]
		public string event_action
		{
			get
			{
				return this._event_action;
			}
			set
			{
				if ((this._event_action != value))
				{
					this.Onevent_actionChanging(value);
					this.SendPropertyChanging();
					this._event_action = value;
					this.SendPropertyChanged("event_action");
					this.Onevent_actionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_visitIp", DbType="Char(30)")]
		public string visitIp
		{
			get
			{
				return this._visitIp;
			}
			set
			{
				if ((this._visitIp != value))
				{
					this.OnvisitIpChanging(value);
					this.SendPropertyChanging();
					this._visitIp = value;
					this.SendPropertyChanged("visitIp");
					this.OnvisitIpChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_location", DbType="VarChar(100)")]
		public string location
		{
			get
			{
				return this._location;
			}
			set
			{
				if ((this._location != value))
				{
					this.OnlocationChanging(value);
					this.SendPropertyChanging();
					this._location = value;
					this.SendPropertyChanged("location");
					this.OnlocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_locationsina", DbType="VarChar(100)")]
		public string locationsina
		{
			get
			{
				return this._locationsina;
			}
			set
			{
				if ((this._locationsina != value))
				{
					this.OnlocationsinaChanging(value);
					this.SendPropertyChanging();
					this._locationsina = value;
					this.SendPropertyChanged("locationsina");
					this.OnlocationsinaChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.piwik_log_reffer")]
	public partial class piwik_log_reffer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _refferurl;
		
		private string _source;
		
		private string _medium;
		
		private int _Id;
		
		private string _refergbk;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnrefferurlChanging(string value);
    partial void OnrefferurlChanged();
    partial void OnsourceChanging(string value);
    partial void OnsourceChanged();
    partial void OnmediumChanging(string value);
    partial void OnmediumChanged();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnrefergbkChanging(string value);
    partial void OnrefergbkChanged();
    #endregion
		
		public piwik_log_reffer()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_refferurl", DbType="VarChar(5000) NOT NULL", CanBeNull=false)]
		public string refferurl
		{
			get
			{
				return this._refferurl;
			}
			set
			{
				if ((this._refferurl != value))
				{
					this.OnrefferurlChanging(value);
					this.SendPropertyChanging();
					this._refferurl = value;
					this.SendPropertyChanged("refferurl");
					this.OnrefferurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source", DbType="VarChar(5000)")]
		public string source
		{
			get
			{
				return this._source;
			}
			set
			{
				if ((this._source != value))
				{
					this.OnsourceChanging(value);
					this.SendPropertyChanging();
					this._source = value;
					this.SendPropertyChanged("source");
					this.OnsourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_medium", DbType="VarChar(5000)")]
		public string medium
		{
			get
			{
				return this._medium;
			}
			set
			{
				if ((this._medium != value))
				{
					this.OnmediumChanging(value);
					this.SendPropertyChanging();
					this._medium = value;
					this.SendPropertyChanged("medium");
					this.OnmediumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_refergbk", DbType="VarChar(5000)")]
		public string refergbk
		{
			get
			{
				return this._refergbk;
			}
			set
			{
				if ((this._refergbk != value))
				{
					this.OnrefergbkChanging(value);
					this.SendPropertyChanging();
					this._refergbk = value;
					this.SendPropertyChanged("refergbk");
					this.OnrefergbkChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
