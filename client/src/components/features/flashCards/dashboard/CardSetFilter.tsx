import { observer } from "mobx-react-lite"
import { Dropdown, DropdownProps, Segment } from "semantic-ui-react";
import { setFilterOptions, setServerFilterOptions } from "../../../../constants/cardSetFilterOptions";
import { useStore } from "../../../../stores/core/store";
import "../../../../styles/Common.css"

export default observer (function CardSetFilter() {
    const options = Array.from(setFilterOptions.entries()).map(([key, text]) => ({
        key: key,
        text: text,
        value: key
      }));
      const { flashCardStore: { isLoading, setFilter, clearFilter } } = useStore();

    function handleOnChange(e: any, { value }: DropdownProps) {
        const filter = setServerFilterOptions.get(value as string);
        if(filter) {
            setFilter(filter);
        } else {
            clearFilter();
        }
    }
    
    return (
        <Segment textAlign='left' basic className='no-padding-left'>
            <Dropdown
                disabled={ isLoading }
                selection
                options={ options }
                defaultValue={ options[0].value }
                onChange={ handleOnChange }
            />         
        </Segment>
    )
});