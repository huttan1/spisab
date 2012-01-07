	// JAVASCRIPT LISTBOX SELECTION - START 
	/* MoveItem
	* Used to move the items from an input field to an other.
	*/
	function MoveItem(fromFieldName,toFieldName) {
		var tmpOption,insertBeforeOption, tmpValue, text, selectedIndex, fromField, toField, i, fieldStrang;

		fromField = document.getElementById(fromFieldName);
		toField = document.getElementById(toFieldName);
		selectedIndex = fromField.selectedIndex; 
		if (selectedIndex == -1) return;
		i=fromField.options.length-1;

       
		while (i >= 0){
			if(fromField.options[i].selected) {
				text = fromField.options[i].text;
				tmpValue = fromField.options[i].value;
				tmpOption = new Option(text, tmpValue, false, false);
				insertBeforeOption  = findBeforeItem(toField,text);
				try {
				    toField.add(tmpOption,insertBeforeOption);
				} catch (ex) { //IE ONLY;
				    if(insertBeforeOption!=null) {
				        toField.add(tmpOption,insertBeforeOption.index);
				    } else {
				        toField.add(tmpOption); 
				    }
				}
	            fromField.remove(i);
			}
			i--;
		}
	}
	
	/*
	 * Find item to insert before, used to avoid resorting. SortField is slow on large lists.
	 */ 
	function findBeforeItem(selectList, text) {
	    var compText;
	    var i=selectList.options.length-1;
		
		while (i >= 0){
		   compText = selectList.options[i].text;
		   if(compText < text) {
		     i++; //Add on item after this, since this is before the item we want to add.
		     break;
		   }
		   i--;
		}
				
		//Nothing found, add before first item!
		if (i < 0) {
		    i = 0; 
		}
		    
		try{
		   return selectList.options[i];
		} catch (err) {
		   return null;
		}
	}
	
	function MoveColumnSettingsItem(add,fromFieldName,toFieldName) {
		var tmpOption, tmpValue, text, selectedIndex, fromField, toField, i, fieldStrang;
		fromField = document.getElementById(fromFieldName);
		toField = document.getElementById(toFieldName);
		selectedIndex = fromField.selectedIndex; 
		if (selectedIndex == -1) return;
        //if (add || ( fromField.options[selectedIndex].value != 'S_FirstName' && fromField.options[selectedIndex].value != 'S_LastName')) 
        //{
		    i=fromField.options.length-1;
		    while (i >= 0){
			    if(fromField.options[i].selected) {
				    text = fromField.options[i].text;
				    tmpValue = fromField.options[i].value;
				    tmpOption = new Option(text, tmpValue, false, false);
				    toField.options[toField.options.length] = tmpOption;
				    fromField.remove(i);
			    }
			    i--;
		    }
		//}
	}
	
	function MoveAlternativeUp(affectedFieldName) {
	    var affectedField = document.getElementById(affectedFieldName);
	    
		var selectedIndex = affectedField.selectedIndex; 
		if (selectedIndex == -1) return;
        
        	i=affectedField.options.length-1;
        	for (i = affectedField.options.length - 1; i >= 0;  i--) {
        	    if (affectedField.options[i].selected) {
        	        var currentSelectedItem = i;
        	        while (currentSelectedItem >= 0 && affectedField.options[currentSelectedItem].selected) {
        	            currentSelectedItem--;
        	        }
        	        if (currentSelectedItem >= 0) {
        	            var previousOption = affectedField.options[currentSelectedItem];
        	            for (var j = currentSelectedItem; j < i; j++) {
        	                var currentOption = affectedField.options[j + 1];
        	                affectedField.options[j] = new Option(currentOption.text, currentOption.value, false, false);
        	                affectedField.options[j].selected = true;
        	            }
        	            affectedField.options[i] = new Option(previousOption.text, previousOption.value, false, false);
        	            i = currentSelectedItem;
        	        }
        	    }
        	}        
	}
	
	function MoveAlternativeDown(affectedFieldName) {
		var affectedField = document.getElementById(affectedFieldName);
		var selectedIndex = affectedField.selectedIndex; 
		if (selectedIndex == -1) return;

		var maxOptionIndex = affectedField.options.length-1;
		for (i = 0; i < affectedField.options.length; i++) {
		    if (affectedField.options[i].selected) {
		        var currentSelectedItem = i;
		        while (currentSelectedItem < affectedField.options.length && affectedField.options[currentSelectedItem].selected) {
		            currentSelectedItem++;
		        }
		        if (currentSelectedItem < affectedField.options.length) {
		            var previousOption = affectedField.options[currentSelectedItem];
		            for (var j = currentSelectedItem; j > i; j--) {
		                var currentOption = affectedField.options[j - 1];
		                affectedField.options[j] = new Option(currentOption.text, currentOption.value, false, false);
		                affectedField.options[j].selected = true;
		            }
		            affectedField.options[i] = new Option(previousOption.text, previousOption.value, false, false);
		            i = currentSelectedItem;
		        }
		    }
		}        
	}

   /*
	* SortField
	* Sorts the fields in alphabetical order
	*/
	function SortField(fieldName) {
		var tmpOption, tmpValueArray, sortField, sortedField, i, sortArray;
	    sortField = document.getElementById(fieldName);
		
		i=0;
		sortArray=new Array();
		
		//  Create en array containing {text:value}-strings. Empty the selected field. 
		while (i < sortField.options.length) {
			sortArray[i] = sortField.options[i].text + "§" + sortField.options[i].value;
			i++;
		}
		
		//  Sort the array.
		sortArray.sort(function (a, b) {
		    var item1 = a.toLowerCase();
		    var item2 = b.toLowerCase();
		    if (item1 < item2) //sort string ascending
		        return -1;
		    if (item1 > item2)
		        return 1;
		    return 0; //default return value (no sorting)
		}
        );
		
		
		// Update selected field with the values from the sorted array. 
		for (i=0;i < sortArray.length; i++) {
			tmpValueArray = sortArray[i].split("§");
			tmpOption = new Option(tmpValueArray[0], tmpValueArray[1], false, false);
			sortField.options[i] = tmpOption;		
		}
	}
	
	/* 
	 * StoreSelectedItems	
	 * Store the value of the selected fields into a hidden field as delimiterStr separated string.
	 */
	function StoreSelectedItems(selectedFieldName, hiddenFieldName, delimiterStr) {
		var selectedField, hiddenField, hiddenFieldString
		selectedField = document.getElementById(selectedFieldName);
		hiddenField = document.getElementById(hiddenFieldName);
		
		i = 0;
		hiddenFieldString = "";
		while (i < selectedField.options.length) {
			hiddenFieldString += selectedField.options[i].value + delimiterStr;
			i++;
		}
		hiddenField.value = hiddenFieldString;
	}
	
	//Moving an existing role when assigning roles to a person.
	function MoveExistingRoles(ExistingOrgs, ExistingRoles, SelectedRoles, Delimiter2) {
		var existingOrgs = document.getElementById(ExistingOrgs);
		var existingRoles = document.getElementById(ExistingRoles);
		var selectedRoles = document.getElementById(SelectedRoles);
		var selectedOrgsIndex = existingOrgs.selectedIndex; 
		var selectedRolesIndex = existingRoles.selectedIndex; 
		if (selectedOrgsIndex == -1 || selectedRolesIndex == -1) return;
		else{
		    var tmpValue = existingOrgs.options[selectedOrgsIndex].value + Delimiter2 + existingRoles.options[selectedRolesIndex].value;
		    var foundInSelectedList = false;
		    for (var i=(selectedRoles.options.length-1);i>=0;i--){
		        if (selectedRoles.options[i].value == tmpValue) foundInSelectedList = true;
		    }
		    if (!foundInSelectedList){
                var tmpText = existingRoles.options[selectedRolesIndex].text + " (" + existingOrgs.options[selectedOrgsIndex].text + ")";
		        var tmpOption = new Option(tmpText, tmpValue, false, false);
		        selectedRoles.options[selectedRoles.options.length] = tmpOption;
		        existingRoles.remove(selectedRolesIndex);
		    }
		}
		SortField(SelectedRoles);
	}
	
	//Removing a selected role when assigning roles to a person.
	function RemoveSelectedRoles(ExistingOrgs, ExistingRoles, SelectedRoles, HiddenExistingRoles, Delimiter1, Delimiter2){
		var existingOrgs = document.getElementById(ExistingOrgs);
		var existingRoles = document.getElementById(ExistingRoles);
		var selectedRoles = document.getElementById(SelectedRoles);
		var selectedIndex = selectedRoles.selectedIndex; 
		if (selectedIndex == -1) return;
		else selectedRoles.remove(selectedIndex);
		LoadExistingRoles(ExistingOrgs, ExistingRoles, SelectedRoles, HiddenExistingRoles, Delimiter1, Delimiter2);
	}
	
	//Loading existing roles when assigning roles to a person.
	function LoadExistingRoles(ExistingOrgs, ExistingRoles, SelectedRoles, HiddenExistingRoles, Delimiter1, Delimiter2){
		var existingOrgs = document.getElementById(ExistingOrgs);
		var existingRoles = document.getElementById(ExistingRoles);
		var selectedRoles = document.getElementById(SelectedRoles);
		var hiddenExistingRoles = document.getElementById(HiddenExistingRoles);
		for (i=(existingRoles.options.length-1);i>=0;i--){
            existingRoles.options[i] = null;
        }
		var split1 = hiddenExistingRoles.value.split(Delimiter1);
		var foundInSelectedList;
		for (var i=0;i<split1.length;i++){
		    foundInSelectedList = false;
		    var split2 = split1[i].split(Delimiter2);
		    if (split2.length == 2){
		        for (var j=(selectedRoles.options.length-1);j>=0;j--){
		            if (selectedRoles.options[j].value == (existingOrgs.options[existingOrgs.selectedIndex].value + Delimiter2 + split2[1])) foundInSelectedList = true;
		        }
		        if (!foundInSelectedList){
		            var tmpText = split2[0];
		            var tmpValue = split2[1];
		            var tmpOption = new Option(tmpText, tmpValue, false, false);
		            existingRoles.options[existingRoles.options.length] = tmpOption;
		        }
		    }
		}
		SortField(ExistingRoles);
	}
	
	function IE6Notify(fieldName,notifyFieldName) {
		var field = document.getElementById(fieldName);
		var notify = document.getElementById(notifyFieldName);
		var selectedIndex = field.selectedIndex; 
		
		if (selectedIndex == -1) 
		    notify.innerHTML = "";
		else
		    notify.innerHTML = field.options[selectedIndex].text
	}
// JAVASCRIPT LISTBOX SELECTION - END