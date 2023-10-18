import { SyntheticEvent } from "react";
import { Button, Icon, StrictButtonProps } from "semantic-ui-react";

interface Props {
    isFavorite: boolean,
    isSubmitting: boolean,
    fieldProps?: StrictButtonProps,
    onClick: (event: SyntheticEvent) => void
}

export const FavoriteButtonComponent = ({ isFavorite, isSubmitting, fieldProps, onClick }: Props) => {
    return (
        <Button 
            { ...fieldProps }
            disabled={ isSubmitting }
            onClick={ event => onClick(event) }
        >
            <Icon
                fitted
                name={ isFavorite ? 'star' : 'star outline' } 
                color={ isFavorite ? 'yellow' : 'grey' }    
            />
        </Button>
    )
};