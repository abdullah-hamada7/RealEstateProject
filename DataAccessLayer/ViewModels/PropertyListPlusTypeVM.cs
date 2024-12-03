namespace DataAccessLayer.ViewModels;

public class PropertyListPlusTypeVM
    {
        public List<TypeVM> types { get; set; }
        public List<ChoiceVM> choices { get; set; }
        public List<PropertyVM> propertyList { get; set; }
        public PropertyTypesCountVM propertyTypesCountVM { get; set; }

    }

