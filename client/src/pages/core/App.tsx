import { Button, Container, Segment } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/core/store';
import LoginForm from '../features/auth/LoginForm';
import ModalContainer from '../common/modals/ModalContainer';
import '../../styles/Home.css'
import { Outlet } from 'react-router-dom';

export default observer(function App() {
  const { accountStore: authStore, modalStore } = useStore();

  return (
    <Segment inverted textAlign='center' vertical className='masthead'>
      <ModalContainer />
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