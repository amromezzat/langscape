import { ReactNode } from "react";
import { Popup } from "semantic-ui-react";

interface Props {
    trigger: ReactNode,
    touched: boolean,
    error: string | undefined
}

export default function ErrorPopup({trigger, touched, error}: Props) {
    return <Popup
            size='mini'
            className='red-popup'
            open={touched && !!error}
            content={error}
            trigger={trigger}  
            position='top right'
    />
}