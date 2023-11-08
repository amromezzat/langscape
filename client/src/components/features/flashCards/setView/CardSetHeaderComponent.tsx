import { Header, Segment } from "semantic-ui-react";

interface Props {
    setName: string;
}

export default function CardSetHeaderComponent({ setName }: Props) {
    return (
        <Segment basic textAlign='left' className='no-padding-left-segment'>
            <Header as='h1' textAlign='left' className='white-text'>
                { setName }
            </Header>
        </Segment>
    )
}