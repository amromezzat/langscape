import { Grid } from "semantic-ui-react";
import CardSetComponent from "./CardSetComponent";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/StoreProvider";
import { useEffect } from "react";

export default observer (function CardGroupComponent() {
    const {flashCardStore} = useStore();
    const {cardsSets, loadSets, loading} = flashCardStore;

    useEffect(() => {
        if (cardsSets.length === 0) {
            loadSets();
        }
      }, [flashCardStore, loadSets, cardsSets]);
    
    return (
        <Grid textAlign={"center"}>
            {!loading && cardsSets.map((cardSet) => (
                <Grid.Column mobile={16} tablet={8} computer={4} key={cardSet.id} >
                    <CardSetComponent cardSet={cardSet}/>
                </Grid.Column>
            ))}
        </Grid>
    )
});