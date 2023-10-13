import { Button, Icon, StrictButtonProps } from "semantic-ui-react";

interface Props {
    isFavorite: boolean,
    isSubmitting: boolean,
    fieldProps?: StrictButtonProps,
    onClick: () => void
}

export const FavoriteButtonComponent = ({ isFavorite, isSubmitting, fieldProps, onClick }: Props) => {
    return (
        <Button 
            { ...fieldProps }
            disabled={ isSubmitting }
            onClick={ onClick }
        >
            <Icon
                fitted
                name={ isFavorite ? 'star' : 'star outline' } 
                color={ isFavorite ? 'yellow' : 'grey' }    
            />
        </Button>
    )
};