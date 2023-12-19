import { Button, Container, Segment } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/core/store';
import LoginForm from '../features/auth/LoginForm';
import ModalContainer from '../common/modals/ModalContainer';
import { Outlet } from 'react-router-dom';
import ConfirmationPrompt from '../common/Prompts/ConfirmationPrompt';
import '../../styles/Home.css'

export default observer(function App() {
  const { accountStore: authStore, modalStore } = useStore();

  return (
    <Segment inverted textAlign='center' vertical className='masthead'>
      <ModalContainer />
      <ConfirmationPrompt />
      <Container style={{ marginTop: '7em' }}>
        {
          authStore.isLoggedIn ? 
            <Outlet /> :
            <Button onClick={ () => modalStore.openModal(<LoginForm urlRoute='/sets' />) } size='huge' inverted>
              Login
            </Button>
        }
      </Container>
    </Segment>
  );
})