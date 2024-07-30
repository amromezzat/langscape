import { observer } from "mobx-react-lite"
import { Dropdown, DropdownProps } from "semantic-ui-react";
import { setFilterOptions, setFilterType } from "../../../../constants/cardSetFilterOptions";
import { useStore } from "../../../../stores/core/store";
import { useNavigate } from "react-router-dom";
import "../../../../styles/Common.css"

export default observer (function CardSetFilter() {
    const options = Array.from(setFilterOptions.entries()).map(([key, text]) => ({
        key: key,
        text: text,
        value: key
    }));
    const { flashCardStore: { isLoading, setFilter, clearFilter, menuFilter }} = useStore();
    const navigate = useNavigate();

    function handleOnChange(_e: any, { value }: DropdownProps) {
        navigate('/sets');
        const filter = value as setFilterType;
        if(filter) {
            setFilter(filter);
        } else {
            clearFilter();
        }
    }
    
    return (
        <Dropdown 
            text='Sets' 
            pointing='top right'
            disabled={ isLoading }
            options={ options }
            onChange={ handleOnChange }
            value={ menuFilter }
            selectOnBlur={false}
        />  
    )
});