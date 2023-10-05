import { Button, Header, Icon, Segment } from "semantic-ui-react";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header>
                <Icon name="search" />
                Oops - we couldn't find what you're looking for
            </Header>
        </Segment>
    )
}