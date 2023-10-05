import React from 'react'
import { Card, Segment } from 'semantic-ui-react'

const MaximizedCardComponent = () => (
    <Card>
        <Card.Content>
            <Card.Header>Question</Card.Header>
            <Card.Description>{"test"}</Card.Description>
        </Card.Content>
        <Card.Content extra>
            <Card.Header>Answer</Card.Header>
            <Card.Description>{"test answer"}</Card.Description>
        </Card.Content>
    </Card>
);

export default MaximizedCardComponent;
