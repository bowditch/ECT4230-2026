using UnityEngine;

public class CoreFeatures : MonoBehaviour
{
   //Accessors - Accessing code outside of this specific class
   //This can be done with public variables, this is a more condensed approach.
   //PROPERTIES - Encapuslates filed
   //GET Accessor (READ) - returns encapsulated values
   //SET Accessor (WRITE) - allocates/assigns new values to field.
   //Note: Must use Pascal case for Property - example: PropertyName

    public bool AudioSFXSourceCreated { get; set; }

   
}
