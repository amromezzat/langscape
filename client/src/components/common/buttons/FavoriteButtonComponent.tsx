import { Button, StrictButtonProps } from "semantic-ui-react";

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
            icon={ isFavorite ? 'star yellow' : 'star outline' }
            disabled={ isSubmitting }
            onClick={ onClick }
        />
    )
};