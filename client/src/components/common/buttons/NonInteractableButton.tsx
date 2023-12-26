import { Button, ButtonProps } from "semantic-ui-react";
import styled from "styled-components";

const InternalNonInteractableButton = styled(Button)`
    pointer-events: none;
`;

export default function NonInteractableButton(props: ButtonProps) {
    return <InternalNonInteractableButton
        {...props}
    />
}