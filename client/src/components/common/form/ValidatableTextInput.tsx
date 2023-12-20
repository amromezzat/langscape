import { useField } from 'formik';
import { Form, Input, InputProps } from 'semantic-ui-react';
import '../../../styles/Common.css'
import ErrorPopup from './ErrorPopup';

export default function ValidatableTextInput(props: InputProps) {
    const [field, meta] = useField(props.name);
    return (
        <Form.Field error={meta.touched && !!meta.error}>    
            <ErrorPopup
                trigger={<Input {...field} {...props} />} 
                touched={meta.touched} 
                error={meta.error}            
            />
        </Form.Field>
    )
}