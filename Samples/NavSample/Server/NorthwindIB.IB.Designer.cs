// ------------------------------------------------------------------------------
// DO NOT MODIFY THIS CLASS - AutoGenerated Code
//    Any changes made to this code will be lost the next time this 
//    code is regenerated.
// 
//    Generated at: 9/26/2012 8:37:50 PM
//    DevForce version: 7.0.0.0
//    Template version: 2.1.4
// ------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using IbEm   = IdeaBlade.EntityModel;
using IbCore = IdeaBlade.Core;
using IbVal  = IdeaBlade.Validation;

[module: IbCore.IdeaBladeLicense("Nvhn3NhKxtlSNzCWdJlo60yUjfSIu1VvU377pnHQpdMU1u65FFn5yBLEtIoaTdCq")]

namespace NavSample { 

  #region NorthwindIBEntities

  /// <summary>
  /// The domain-specific EntityManager for your domain model.
  /// </summary>
  public partial class NorthwindIBEntities : IbEm.EntityManager {

    #region Constructors

    /// <summary>See <see cref="M:IbEm.EntityManager.#ctor(bool, string, IbEm.EntityServiceOption, string)"/>. </summary>
    public NorthwindIBEntities(bool shouldConnect=true, string dataSourceExtension=null, IbEm.EntityServiceOption entityServiceOption=IbEm.EntityServiceOption.UseDefaultService, string compositionContextName=null) : base(shouldConnect, dataSourceExtension, entityServiceOption, compositionContextName) {}

    /// <summary>See <see cref="M:IbEm.EntityManager.#ctor(IbEm.EntityManagerContext)"/>. </summary>
    public NorthwindIBEntities(IbEm.EntityManagerContext entityManagerContext) : base(entityManagerContext) {}

    /// <summary>See <see cref="M:IbEm.EntityManager.#ctor(IbEm.EntityManager, bool, string, IbEm.EntityServiceOption, string)"/>. </summary>
    public NorthwindIBEntities(IbEm.EntityManager entityManager, bool shouldConnect, string dataSourceExtension=null, IbEm.EntityServiceOption entityServiceOption=IbEm.EntityServiceOption.UseDefaultService, string compositionContextName=null) : base(entityManager, shouldConnect, dataSourceExtension, entityServiceOption, compositionContextName) {}

    /// <summary>See <see cref="M:IbEm.EntityManager.#ctor(IbEm.EntityManager, IbEm.EntityManagerContext)"/>. </summary>
    public NorthwindIBEntities(IbEm.EntityManager entityManager, IbEm.EntityManagerContext entityManagerContext=null) : base(entityManager, entityManagerContext) {}

    static NorthwindIBEntities() {
      IbEm.EntityRelation.InitializeEntityRelations(IdeaBlade.Core.Reflection.ReflectionExtensions.GetAssembly(typeof(NorthwindIBEntities)));
    }
    #endregion Constructors

    #region EntityQueries

    /// <summary>Gets the <see cref="T:IbEm.EntityQuery"/> associated with the given EntitySet name. </summary>
    public IbEm.EntityQuery<Customer> Customers {
      get { return new IbEm.EntityQuery<Customer>("Customers", this); }
    }
    #endregion EntityQueries
  }
  #endregion NorthwindIBEntities

  #region Customer class

  /// <summary>The auto-generated Customer class. </summary>
  [DataContract(IsReference=true)]
  [IbEm.DataSourceKeyName(@"NorthwindIBEntities")]
  [IbEm.DefaultEntitySetName(@"NorthwindIBEntities.Customers")]
  public partial class Customer : IbEm.Entity {

    /// <summary>Returns the property path for the given expression. </summary>
    /// <example>
    /// Usage:
    /// <code>
    ///    var r = Employee.PathFor(e => e.Manager.City); // returns "Manager.City"
    /// </code>
    /// </example>
    public static string PathFor(System.Linq.Expressions.Expression<System.Func<Customer, object>> expr) {
      return IbCore.PropertyPath.For<Customer>(expr);
    }

    #region Data Properties

    /// <summary>Gets or sets the CustomerID. </summary>
    [Key]
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="CustomerID", AutoGenerateField=true)]
    [IbVal.RequiredValueVerifier( ErrorMessageResourceName="Customer_CustomerID")]
    public System.Guid CustomerID {
      get { return PropertyMetadata.CustomerID.GetValue(this); }
      set { PropertyMetadata.CustomerID.SetValue(this, value); }
    }

    /// <summary>Gets or sets the CustomerID_OLD. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="CustomerID_OLD", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=5, IsRequired=false, ErrorMessageResourceName="Customer_CustomerID_OLD")]
    public string CustomerID_OLD {
      get { return PropertyMetadata.CustomerID_OLD.GetValue(this); }
      set { PropertyMetadata.CustomerID_OLD.SetValue(this, value); }
    }

    /// <summary>Gets or sets the CompanyName. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="CompanyName", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=40, IsRequired=true, ErrorMessageResourceName="Customer_CompanyName")]
    public string CompanyName {
      get { return PropertyMetadata.CompanyName.GetValue(this); }
      set { PropertyMetadata.CompanyName.SetValue(this, value); }
    }

    /// <summary>Gets or sets the ContactName. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="ContactName", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=30, IsRequired=false, ErrorMessageResourceName="Customer_ContactName")]
    public string ContactName {
      get { return PropertyMetadata.ContactName.GetValue(this); }
      set { PropertyMetadata.ContactName.SetValue(this, value); }
    }

    /// <summary>Gets or sets the ContactTitle. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="ContactTitle", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=30, IsRequired=false, ErrorMessageResourceName="Customer_ContactTitle")]
    public string ContactTitle {
      get { return PropertyMetadata.ContactTitle.GetValue(this); }
      set { PropertyMetadata.ContactTitle.SetValue(this, value); }
    }

    /// <summary>Gets or sets the Address. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="Address", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=60, IsRequired=false, ErrorMessageResourceName="Customer_Address")]
    public string Address {
      get { return PropertyMetadata.Address.GetValue(this); }
      set { PropertyMetadata.Address.SetValue(this, value); }
    }

    /// <summary>Gets or sets the City. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="City", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=15, IsRequired=false, ErrorMessageResourceName="Customer_City")]
    public string City {
      get { return PropertyMetadata.City.GetValue(this); }
      set { PropertyMetadata.City.SetValue(this, value); }
    }

    /// <summary>Gets or sets the Region. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="Region", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=15, IsRequired=false, ErrorMessageResourceName="Customer_Region")]
    public string Region {
      get { return PropertyMetadata.Region.GetValue(this); }
      set { PropertyMetadata.Region.SetValue(this, value); }
    }

    /// <summary>Gets or sets the PostalCode. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="PostalCode", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=10, IsRequired=false, ErrorMessageResourceName="Customer_PostalCode")]
    public string PostalCode {
      get { return PropertyMetadata.PostalCode.GetValue(this); }
      set { PropertyMetadata.PostalCode.SetValue(this, value); }
    }

    /// <summary>Gets or sets the Country. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="Country", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=15, IsRequired=false, ErrorMessageResourceName="Customer_Country")]
    public string Country {
      get { return PropertyMetadata.Country.GetValue(this); }
      set { PropertyMetadata.Country.SetValue(this, value); }
    }

    /// <summary>Gets or sets the Phone. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="Phone", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=24, IsRequired=false, ErrorMessageResourceName="Customer_Phone")]
    public string Phone {
      get { return PropertyMetadata.Phone.GetValue(this); }
      set { PropertyMetadata.Phone.SetValue(this, value); }
    }

    /// <summary>Gets or sets the Fax. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="Fax", AutoGenerateField=true)]
    [IbVal.StringLengthVerifier(MaxValue=24, IsRequired=false, ErrorMessageResourceName="Customer_Fax")]
    public string Fax {
      get { return PropertyMetadata.Fax.GetValue(this); }
      set { PropertyMetadata.Fax.SetValue(this, value); }
    }

    /// <summary>Gets or sets the RowVersion. </summary>
    [DataMember]
    [Bindable(true, BindingDirection.TwoWay)]
    [Editable(true)]
    [Display(Name="RowVersion", AutoGenerateField=true)]
    public System.Nullable<int> RowVersion {
      get { return PropertyMetadata.RowVersion.GetValue(this); }
      set { PropertyMetadata.RowVersion.SetValue(this, value); }
    }
    #endregion Data Properties

    #region Navigation properties
    #endregion Navigation properties

    #region EntityProperty definitions
    public partial class PropertyMetadata {

      /// Explicit static constructor ensures static fields are initialized.
      static PropertyMetadata() {}

      /// <summary>The CustomerID <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, System.Guid> CustomerID = new IbEm.DataEntityProperty<Customer, System.Guid>("CustomerID", false, true, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The CustomerID_OLD <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> CustomerID_OLD = new IbEm.DataEntityProperty<Customer, string>("CustomerID_OLD", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The CompanyName <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> CompanyName = new IbEm.DataEntityProperty<Customer, string>("CompanyName", false, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The ContactName <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> ContactName = new IbEm.DataEntityProperty<Customer, string>("ContactName", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The ContactTitle <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> ContactTitle = new IbEm.DataEntityProperty<Customer, string>("ContactTitle", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The Address <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> Address = new IbEm.DataEntityProperty<Customer, string>("Address", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The City <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> City = new IbEm.DataEntityProperty<Customer, string>("City", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The Region <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> Region = new IbEm.DataEntityProperty<Customer, string>("Region", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The PostalCode <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> PostalCode = new IbEm.DataEntityProperty<Customer, string>("PostalCode", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The Country <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> Country = new IbEm.DataEntityProperty<Customer, string>("Country", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The Phone <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> Phone = new IbEm.DataEntityProperty<Customer, string>("Phone", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The Fax <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, string> Fax = new IbEm.DataEntityProperty<Customer, string>("Fax", true, false, IbEm.ConcurrencyStrategy.None, false, null);

      /// <summary>The RowVersion <see cref="T:IbEm.DataEntityProperty"/>. </summary>
      public static readonly IbEm.DataEntityProperty<Customer, System.Nullable<int>> RowVersion = new IbEm.DataEntityProperty<Customer, System.Nullable<int>>("RowVersion", true, false, IbEm.ConcurrencyStrategy.None, false, null);
    }
    #endregion EntityProperty definitions

    #region EntityPropertyNames
    public new partial class EntityPropertyNames : IbEm.Entity.EntityPropertyNames {
      public const String CustomerID = "CustomerID";
      public const String CustomerID_OLD = "CustomerID_OLD";
      public const String CompanyName = "CompanyName";
      public const String ContactName = "ContactName";
      public const String ContactTitle = "ContactTitle";
      public const String Address = "Address";
      public const String City = "City";
      public const String Region = "Region";
      public const String PostalCode = "PostalCode";
      public const String Country = "Country";
      public const String Phone = "Phone";
      public const String Fax = "Fax";
      public const String RowVersion = "RowVersion";
    }
    #endregion EntityPropertyNames
  }
  #endregion Customer class

  #region EntityRelations

  /// <summary>
  /// A generated class that returns the relations between entities in this model.
  /// </summary>
  [IbCore.IdeaBladeGuid("c8672da5-ffbf-4b3d-87b6-3d6de9f0c1b7", "2.1.4")]
  public partial class EntityRelations : IbEm.IEntityRelations {

    /// Explicit static constructor ensures static fields are initialized.
    static EntityRelations() {}
  }
  #endregion EntityRelations
}
