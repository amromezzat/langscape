import { ErrorMessage, Form, Formik, FormikHelpers } from "formik"
import { Button, Header, Label } from "semantic-ui-react"
import { observer } from "mobx-react-lite"
import { useStore } from "../../../stores/core/store"
import ValidatableTextInput from "../../common/form/ValidatableTextInput"
import { AuthUserForm } from "../../../models/user/authUserForm"
import { router } from "../../../routes/Routes"

export default observer(function LoginForm() {
    const { authStore, modalStore } = useStore();
    const initialValues: AuthUserForm = {
        email: '',
        password: '',
        error: undefined
     };

    async function onFormSubmit(formData: AuthUserForm, {setErrors}: FormikHelpers<AuthUserForm>) {
        try {
            await authStore.login(formData);
            router.navigate('/');
            modalStore.closeModal();
        } catch (e){
            setErrors({error: 'Invalid email or password!!'});
        }
    }

    return (
        <Formik
            initialValues={initialValues}
            onSubmit={onFormSubmit}
        >
            {({handleSubmit, isSubmitting, errors}) => (
                <Form className='ui form' onSubmit={handleSubmit} autoComplete='off' >
                    <Header as='h2' content='Login' color='teal' textAlign='center' />
                    <ValidatableTextInput placeholder="Email" name='email' />
                    <ValidatableTextInput placeholder="Password" name='password' type='password' />
                    <ErrorMessage 
                        name='error' render={() => <Label style={{marginBottom: 10}} basic color='red' content={errors.error}/>}
                    />
                    <Button loading={isSubmitting} positive content='Login' type="submit" fluid />
                </Form>
            )}
        </Formik>
    )
})