﻿using System.Xml.Serialization;

namespace JediNinja.Controls.WP
{


    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class GeocodeResponse
    {

        private string statusField;

        private GeocodeResponseResult[] resultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("result", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResult[] result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResult
    {

        private string typeField;

        private string formatted_addressField;

        private GeocodeResponseResultAddress_component[] address_componentField;

        private GeocodeResponseResultGeometry[] geometryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string formatted_address
        {
            get
            {
                return this.formatted_addressField;
            }
            set
            {
                this.formatted_addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("address_component", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultAddress_component[] address_component
        {
            get
            {
                return this.address_componentField;
            }
            set
            {
                this.address_componentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("geometry", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultGeometry[] geometry
        {
            get
            {
                return this.geometryField;
            }
            set
            {
                this.geometryField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultAddress_component
    {

        private string long_nameField;

        private string short_nameField;

        private GeocodeResponseResultAddress_componentType[] typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string long_name
        {
            get
            {
                return this.long_nameField;
            }
            set
            {
                this.long_nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string short_name
        {
            get
            {
                return this.short_nameField;
            }
            set
            {
                this.short_nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("type", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public GeocodeResponseResultAddress_componentType[] type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultAddress_componentType
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultGeometry
    {

        private string location_typeField;

        private GeocodeResponseResultGeometryLocation[] locationField;

        private GeocodeResponseResultGeometryViewport[] viewportField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string location_type
        {
            get
            {
                return this.location_typeField;
            }
            set
            {
                this.location_typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("location", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultGeometryLocation[] location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("viewport", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultGeometryViewport[] viewport
        {
            get
            {
                return this.viewportField;
            }
            set
            {
                this.viewportField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultGeometryLocation
    {

        private string latField;

        private string lngField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultGeometryViewport
    {

        private GeocodeResponseResultGeometryViewportSouthwest[] southwestField;

        private GeocodeResponseResultGeometryViewportNortheast[] northeastField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("southwest", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultGeometryViewportSouthwest[] southwest
        {
            get
            {
                return this.southwestField;
            }
            set
            {
                this.southwestField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("northeast", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GeocodeResponseResultGeometryViewportNortheast[] northeast
        {
            get
            {
                return this.northeastField;
            }
            set
            {
                this.northeastField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultGeometryViewportSouthwest
    {

        private string latField;

        private string lngField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GeocodeResponseResultGeometryViewportNortheast
    {

        private string latField;

        private string lngField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {

        private GeocodeResponse[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GeocodeResponse")]
        public GeocodeResponse[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}