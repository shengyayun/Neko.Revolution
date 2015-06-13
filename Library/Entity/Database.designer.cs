﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.Entity
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="sq_langdaren")]
	public partial class DatabaseContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertLexicon(Lexicon instance);
    partial void UpdateLexicon(Lexicon instance);
    partial void DeleteLexicon(Lexicon instance);
    #endregion
		
		public DatabaseContext() : 
				base(global::Library.Properties.Settings.Default.sq_langdarenConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Lexicon> Lexicon
		{
			get
			{
				return this.GetTable<Lexicon>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Lexicon")]
	public partial class Lexicon : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _LxKey;
		
		private string _LxValue;
		
		private short _LxType;
		
		private System.DateTime _Time;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnLxKeyChanging(string value);
    partial void OnLxKeyChanged();
    partial void OnLxValueChanging(string value);
    partial void OnLxValueChanged();
    partial void OnLxTypeChanging(short value);
    partial void OnLxTypeChanged();
    partial void OnTimeChanging(System.DateTime value);
    partial void OnTimeChanged();
    #endregion
		
		public Lexicon()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LxKey", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string LxKey
		{
			get
			{
				return this._LxKey;
			}
			set
			{
				if ((this._LxKey != value))
				{
					this.OnLxKeyChanging(value);
					this.SendPropertyChanging();
					this._LxKey = value;
					this.SendPropertyChanged("LxKey");
					this.OnLxKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LxValue", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string LxValue
		{
			get
			{
				return this._LxValue;
			}
			set
			{
				if ((this._LxValue != value))
				{
					this.OnLxValueChanging(value);
					this.SendPropertyChanging();
					this._LxValue = value;
					this.SendPropertyChanged("LxValue");
					this.OnLxValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LxType", DbType="SmallInt NOT NULL")]
		public short LxType
		{
			get
			{
				return this._LxType;
			}
			set
			{
				if ((this._LxType != value))
				{
					this.OnLxTypeChanging(value);
					this.SendPropertyChanging();
					this._LxType = value;
					this.SendPropertyChanged("LxType");
					this.OnLxTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time", DbType="DateTime NOT NULL")]
		public System.DateTime Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				if ((this._Time != value))
				{
					this.OnTimeChanging(value);
					this.SendPropertyChanging();
					this._Time = value;
					this.SendPropertyChanged("Time");
					this.OnTimeChanged();
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