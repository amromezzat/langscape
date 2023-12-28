import { Button, Container, Menu, Dropdown, Icon } from 'semantic-ui-react';
import { NavLink, useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../stores/core/store';
import CardSetFilter from '../../../components/features/flashCards/dashboard/CardSetFilter';
import { setFilterType } from '../../../constants/cardSetFilterOptions';
import { router } from '../../../routes/Routes';
import '../../../styles/MenuBar.css'

export default observer(function NavigationBar() {
    const {accountStore: { user: authUser, logout }, flashCardStore: { setFilter, clearFilter }} = useStore();
    const navigate = useNavigate();

    function handleShowUserSet() {
        navigate(`/sets`);
        setFilter(setFilterType.created);
    }

    function handleLogout() {
        logout();
        clearFilter();
        router.navigate('/');
    }

    return (
        <Menu inverted fixed='top' className='menu-bar'>
            <Container>
                <Menu.Item header>
                    <Icon name='pied piper alternate' size='big' style={{ marginRight: '10px' }} />
                    LangEscape
                </Menu.Item>
                <Menu.Item>
                    <CardSetFilter />
                </Menu.Item>
                <Menu.Item>
                    <Button 
                        circular 
                        as={NavLink} to='/create-set' 
                        positive 
                        content='Create Set' 
                        icon='plus' 
                    />
                </Menu.Item>
                <Menu.Item position='right'>
                    <Dropdown pointing='top left' text={authUser?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={handleShowUserSet} text='My Sets' icon='list alternate' />
                            <Dropdown.Item onClick={handleLogout} text='Logout' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})