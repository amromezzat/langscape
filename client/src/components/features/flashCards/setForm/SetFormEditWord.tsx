import { Button, Grid, Segment } from "semantic-ui-react";
import ValidatableTextInput from "../../../common/form/ValidatableTextInput";
import "../../../../styles/Common.css"

interface Props {
    wordInputName: string
    translationInputName: string
    index: number
    isDeleted: boolean
    handleRemove: () => void
}

export default function SetFormEditWord({wordInputName, translationInputName, index, isDeleted, handleRemove}: Props) {
    return ( 
        <Segment>
            <Grid verticalAlign='middle' divided='vertically'>
                <Grid.Row className='no-padding-bot'>
                    <Grid.Column floated='left' textAlign='left' >
                        <h3>{index}</h3>
                    </Grid.Column>
                    <Grid.Column>
                        {!isDeleted ? 
                            <Button
                                icon='trash alternate'
                                negative
                                compact
                                onClick={() => handleRemove()}
                                type='button'
                            /> :
                            <Button
                                icon='redo'
                                positive
                                compact
                                onClick={() => handleRemove()}
                                type='button'
                            />
                        }
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row columns='equal' className='no-padding-top no-padding-bot'>
                    <Grid.Column>
                        <ValidatableTextInput 
                            name={wordInputName}
                            label={{ tag: true, content: 'word' }} 
                            labelPosition='right'
                            disabled={isDeleted}
                        />
                    </Grid.Column>
                    <Grid.Column>
                        <ValidatableTextInput  
                            name={translationInputName}
                            label={{ tag: true, content: 'translation', color: 'teal' }} 
                            labelPosition='right'
                            disabled={isDeleted}
                        />
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
}