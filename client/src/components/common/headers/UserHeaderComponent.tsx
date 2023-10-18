import { useEffect, useState } from "react";
import { Header, Icon, Loader, Segment } from "semantic-ui-react"
import { User } from "../../../models/user/user";
import { useStore } from "../../../stores/core/store";
import { observer } from "mobx-react-lite";

interface Props {
    username: string;
}

export default observer (function UserHeaderComponent({ username }: Props) {
    const [user, setUser] = useState<User | undefined>(undefined);
    const {accountStore: {getUserByUsername: getUser, usersRegistery}} = useStore();

    useEffect(() => {
        if (!usersRegistery.has(username)) {
            getUser(username);
        } else if (!user) {
            setUser(usersRegistery.get(username));
        }
      }, [getUser, usersRegistery, user, username]);

    return (
        <Segment basic textAlign='left'>
        {user ? (
            <Header as='h2' textAlign='left' className='white-text'>
                <Icon inverted circular name='user' color='blue' /> {user?.displayName}
            </Header>
        ) : (
            <Loader active inline/>
        )}
        </Segment>
    )
});