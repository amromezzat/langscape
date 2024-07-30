import { Button, Icon, Placeholder, Popup, StrictButtonProps } from "semantic-ui-react";
import { FlashCardSetMeta } from "../../../models/flashCards/flashCardSet";
import { useState, SyntheticEvent } from "react";
import { useStore } from "../../../stores/core/store";
import { User } from "../../../models/user/user";

interface Props {
    isDisabled?: boolean,
    buttonProps?: StrictButtonProps,
    setMeta: FlashCardSetMeta,
    onClick?: (event: SyntheticEvent, user: User) => void,
    onOpen?: () => void
}

export const UserInfoComponent = ({ isDisabled, buttonProps, setMeta, onClick, onOpen }: Props) => {
    const { accountStore } = useStore();
    const [ user, setUser ] = useState<User | undefined>(undefined);

    async function handleOnOpen() {
        if(setMeta.createdBy) {
            const user = await accountStore.getUserById(setMeta.createdBy);
            setUser(user);
        }
        onOpen?.();
    }

    function isOwner() {
        return accountStore.isCurrentUser(setMeta.createdBy);
    }

    function handleOnClick(event: SyntheticEvent) {
        if (!isDisabled) {
            onClick?.(event, user!);
        }
    }

    return (
        <Popup
            trigger={ 
                <Button 
                    { ...buttonProps }
                    onClick={ handleOnClick }
                >
                <Icon
                    fitted
                    name={ isOwner() ? 'user' : 'user outline' } 
                    color={ setMeta.createdBy ? 'blue' : 'grey' }
                />
                </Button>
            }
            onOpen={ handleOnOpen }
            popperDependencies={[ !!user?.displayName ]}
            content={        
            <>
            { setMeta.createdBy && 
                <>
                { 
                    user?.displayName ? (
                        <p>
                            <strong>Created By: </strong>{ user.displayName }{isOwner() && '(you)'}
                        </p>
                    ) : (
                        <>
                            <Placeholder style={{ minWidth: '200px' }}>
                                <Placeholder.Line length='short' />
                            </Placeholder>
                            <br/>
                        </>
                    )
                }
                </>
                }
                <p>
                    <strong>Created At:</strong> { setMeta.createdAt.toLocaleString() }
                </p>
            </>
            }
        >
        </Popup>
    )
};