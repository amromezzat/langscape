import { Button, Container, Segment } from 'semantic-ui-react';
import FlashCardSetDashboard from '../features/flashcards/FlashCardSetDashboard';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/core/store';
import LoginForm from '../features/auth/LoginForm';
import ModalContainer from '../common/modals/ModalContainer';
import '../../styles/Home.css'

export default observer(function App() {
  const { accountStore: authStore, modalStore } = useStore();

  return (
    <Segment inverted textAlign='center' vertical className='masthead'>
      <ModalContainer />
      <Container style={{ marginTop: '7em' }}>
        {
          authStore.isLoggedIn ? 
            <FlashCardSetDashboard /> :
            <Button onClick={ () => modalStore.openModal(<LoginForm />) } size='huge' inverted>
              Login
            </Button>
        }
      </Container>
    </Segment>
  );
})