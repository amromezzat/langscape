import { useField } from 'formik';
import { Form, Input, InputProps } from 'semantic-ui-react';
import ErrorPopup from './ErrorPopup';
import { useEffect } from 'react';
import '../../../styles/Common.css'

export interface ValidatableInputProps extends InputProps {
    onUpdate?: () => void,
    shouldReset?: boolean
}

export default function ValidatableTextInput(props: ValidatableInputProps) {
    const [field, meta, {setValue}] = useField(props.name);
    const {onUpdate, shouldReset, ...inputProps} = props;

    useEffect(() => {
        if(meta.touched && meta.initialValue !== meta.value) {
            onUpdate?.();
        }
    }, [meta.touched, meta.initialValue, meta.value, onUpdate]);

    useEffect(() => {
        if(shouldReset && meta.initialValue !== meta.value) {
            setValue(meta.initialValue);
        }
    }, [meta.initialValue, meta.value, shouldReset, setValue]);

    return (
        <Form.Field error={meta.touched && !!meta.error}>    
            <ErrorPopup
                trigger={<Input {...field} {...inputProps} />} 
                touched={meta.touched} 
                error={meta.error}            
            />
        </Form.Field>
    )
}