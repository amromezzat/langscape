import { Container, Grid, Header } from "semantic-ui-react";
import CardSetComponent from "./CardSetComponent";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useStore } from "../../../stores/core/store";
import '../../../styles/Common.css';

export default observer (function CardGroupComponent() {
    const {flashCardStore} = useStore();
    const {setsRegistery: cardsSets, loadSets, loading} = flashCardStore;

    useEffect(() => {
        if (cardsSets.size === 0) {
            loadSets();
        }
      }, [flashCardStore, loadSets, cardsSets]);
    
    return (
        <Grid textAlign='left'>
            { !loading && cardsSets.size === 0 && 
                <Container style={{ marginTop: '7em' }}>
                    <Header as='h2' className='white-text'>
                        Nothing to show here!    
                    </Header>
                </Container>    
            }
            { !loading && Array.from(cardsSets).map(([setId, cardSet]) => (
                    <Grid.Column mobile={16} tablet={8} computer={4} key={setId} >
                        <CardSetComponent cardSet={cardSet}/>
                    </Grid.Column>
                )) 
            }
        </Grid>
    )
});