import { Button, Container, Header, Icon, Loader, Segment } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../stores/core/store';
import LoginForm from '../features/auth/LoginForm';
import ModalContainer from '../common/modals/ModalContainer';
import { Outlet, useNavigate } from 'react-router-dom';
import ConfirmationPrompt from '../common/prompts/ConfirmationPrompt';
import NavigationBar from '../common/bars/MenuBar';
import RegisterForm from '../features/auth/RegisterForm';
import { setFilterType } from '../../constants/cardSetFilterOptions';
import { useEffect, useState } from 'react';
import '../../styles/Home.css'

export default observer(function App() {
  const { 
    accountStore: { hasActiveSession, user, getCurrentUser }, 
    modalStore, 
    flashCardStore: { setFilter }
  } = useStore();
  const navigate = useNavigate();
  const [ isLoadingUser, setIsLoadingUser ] = useState(true);

  function handleModalRedirect(urlRoute: string) {
    navigate(urlRoute);
    setFilter(setFilterType.all);
    setIsLoadingUser(false);
  }

  useEffect(() => {
    if (!hasActiveSession) {
      setIsLoadingUser(false);
      return;
    }
    if(!user) {
      setIsLoadingUser(true);
      getCurrentUser().finally(() => setIsLoadingUser(false));
    }
  }, [hasActiveSession, user, setIsLoadingUser, getCurrentUser])

  return (
    <Segment inverted textAlign='center' vertical className='masthead'>
      <ModalContainer />
      <ConfirmationPrompt />
      <Container text style={{ marginTop: '7em' }}>
      {
        isLoadingUser ? (
          <Loader active inline />
        ) : (   
          hasActiveSession ? (
            <>
              <NavigationBar />
              <Outlet />
            </>
            ) : (
            <>
              <Icon name='pied piper alternate' size='massive' style={{ marginBottom: '10px' }} />
              <Header as='h1' inverted>
                LangEscape
              </Header>
              <Button onClick={ () => modalStore.openModal(<LoginForm />, () => handleModalRedirect('/sets')) } size='huge' inverted>
                Login
              </Button>
              <Button onClick={ () => modalStore.openModal(<RegisterForm />, () => handleModalRedirect('/sets')) } size='huge' inverted>
                Register
              </Button>
            </>
        ))
      }
      </Container>
    </Segment>
  );
})