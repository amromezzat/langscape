import { Button, Icon, Placeholder, Popup, StrictButtonProps } from "semantic-ui-react";
import { FlashCardSetMeta } from "../../../models/flashCards/flashCardSet";
import { useState } from "react";
import { useStore } from "../../../stores/core/store";

interface Props {
    isDisabled?: boolean,
    buttonProps?: StrictButtonProps,
    setMeta: FlashCardSetMeta,
    onClick?: () => void,
    onOpen?: () => void
}

export const UserInfoComponent = ({ isDisabled, buttonProps, setMeta, onClick, onOpen }: Props) => {
    const { accountStore } = useStore();
    const [ displayName, setDisplayName ] = useState<string | undefined>(undefined);

    async function handleOnOpen() {
        if(setMeta.createdBy) {
            const user = await accountStore.getUser(setMeta.createdBy);
            setDisplayName(user.displayName);
        }
        onOpen?.();
    }

    return (
        <Popup
            trigger={ 
                <Button 
                    { ...buttonProps }
                    disabled={ isDisabled ?? false }
                    onClick={ onClick }
                >
                <Icon
                    fitted
                    name='user' 
                    color='blue' 
                />
                </Button>
            }
            onOpen={ handleOnOpen }
            popperDependencies={[ !!displayName ]}
            content={        
            <>
            { setMeta.createdBy && 
                <>
                { 
                    displayName ? (
                        <p>
                            <strong>Created By: </strong>{ displayName }
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