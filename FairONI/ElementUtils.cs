using System.Collections.Generic;

namespace FairONI
{
    public static class ElementUtils
    {

        public static void AddOreTag(Element element, Tag tag)
        {
            List<Tag> tags = new List<Tag>(element.oreTags) { tag };
            element.oreTags = tags.ToArray();
            GameTags.SolidElements.Add(element.tag);
        }

    }
}
