import { Header, Segment } from "semantic-ui-react";
import '../../../../styles/Common.css'

interface Props {
    setName: string;
}

export default function CardSetHeaderComponent({ setName }: Props) {
    return (
        <Segment basic className='no-padding-left'>
            <Header as='h1' textAlign='left' className='white-text'>
                { setName }
            </Header>
        </Segment>
    )
}