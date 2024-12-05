import {
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,

} from "reactstrap";

import React from "react";

// arrayItems = [{ text: "Item per page: ", value: 5 }];
const DropdownButtonSmall = ({ selectedItemState,
                               viewText = true, 
                               viewValue = false, 
                               arrayItems, 
                               onSelectChange, 
                               color = "primary",
                               size="sm" }) => {
  var [itemSelected, setItemSelected] = selectedItemState;

  const handleSelect = (item) => {
    setItemSelected(item);
    onSelectChange(item.value);
  };

  function buildButtonText(viewText, viewValue, item) {
    var result = "";
    if (viewText) result += item.text;
    if (viewValue) result += item.value;
    return result;
  }

  return (
    <>
      <UncontrolledDropdown>
        <DropdownToggle caret
          color={color}
          size={size}
          outline
        >
          {buildButtonText(viewText, viewValue, itemSelected)}
        </DropdownToggle>
        <DropdownMenu className="dropdown-menu-arrow" right>
          {arrayItems.map((item) => (
            <DropdownItem
              href="#pablo"
              key={"select-size-" + item.value}
              onClick={(e) => {
                e.preventDefault();
                handleSelect(item)
              }}
            >
              {buildButtonText(viewText, viewValue, item)}
            </DropdownItem>
          ))}
        </DropdownMenu>
      </UncontrolledDropdown>
    </>
  );
};

export default DropdownButtonSmall;
