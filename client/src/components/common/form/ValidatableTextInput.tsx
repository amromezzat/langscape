import { useField } from 'formik';
import { Form, Input, LabelProps, Popup, SemanticShorthandItem } from 'semantic-ui-react';
import '../../../styles/Common.css'

interface Props {
    placeholder?: string;
    name: string;
    label?: SemanticShorthandItem<LabelProps>;
    labelPosition?: 'left' | 'right' | 'left corner' | 'right corner'
    type?: string;
    size?: 'mini' | 'small' | 'large' | 'big' | 'huge' | 'massive'
    disabled?: boolean
}

export default function ValidatableTextInput(props: Props) {
    const [field, meta] = useField(props.name);
    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <Popup
                size='mini'
                className='red-popup'
                open={meta.touched && !!meta.error}
                content={meta.error}
                trigger={<Input {...field} {...props} />}
            />       
        </Form.Field>
    )
}