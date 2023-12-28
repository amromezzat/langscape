import { ErrorMessage, Form, Formik, FormikHelpers } from "formik"
import { Button, Header, Label } from "semantic-ui-react"
import { observer } from "mobx-react-lite"
import * as Yup from 'yup';
import { useStore } from "../../../stores/core/store";
import ValidatableTextInput from "../../../components/common/form/ValidatableTextInput";
import { RegisterUserForm } from "../../../models/user/registerUserForm";

export default observer(function RegisterForm() {
    const { accountStore: authStore, modalStore } = useStore();
    const initialValues: RegisterUserForm = {
        email: '',
        password: '',
        displayName: '',
        username: '',
        error: undefined
     };

    async function onFormSubmit(formData: RegisterUserForm, {setErrors}: FormikHelpers<RegisterUserForm>) {
        try {
            await authStore.register(formData);
            modalStore.closeModal(true);
        } catch (e){
            setErrors({error: 'An error occured please try again!'});
        }
    }

    return (
        <Formik
            initialValues={initialValues}
            onSubmit={onFormSubmit}
            validationSchema={
                Yup.object({
                    displayName: Yup.string().required("Display name can't be empty"),
                    username: Yup.string().required("Username can't be empty"),
                    email: Yup.string()
                        .required("Email can't be empty")
                        .email("Please enter a valid email address"),
                    password: Yup.string()
                        .required("Password can't be empty")
                        .test('len', 'Password must be between 4-8 characters', val => val.length >= 4 && val.length <= 8)
                        .matches(/[A-Z]+/, 'Your password must contain at least one uppercase letter')
                        .matches(/[a-z]+/, 'Your password must contain at least one uppercase letter')
                        .matches(/\d+/, 'Your password must contain at least one uppercase letter')
                })
            }
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form' onSubmit={handleSubmit} autoComplete='off' >
                    <Header as='h2' content='Sign up' color='teal' textAlign='center' />
                    <ValidatableTextInput placeholder="Display Name" name='displayName' />
                    <ValidatableTextInput placeholder="Username" name='username' />
                    <ValidatableTextInput placeholder="Email" name='email' />
                    <ValidatableTextInput placeholder="Password" name='password' type='password' />
                    <ErrorMessage 
                        name='error' render={() => <Label style={{marginBottom: 10}} basic color='red' content={errors.error}/>}
                    />
                    <Button 
                        disabled={isSubmitting || !dirty || !isValid}
                        loading={isSubmitting} 
                        positive 
                        content='Register' 
                        type="submit" 
                        fluid 
                    />
                </Form>
            )}
        </Formik>
    )
})