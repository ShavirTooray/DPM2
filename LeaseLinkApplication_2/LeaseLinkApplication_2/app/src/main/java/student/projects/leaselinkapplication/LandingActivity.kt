package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import androidx.appcompat.app.AppCompatActivity

class LandingActivity : AppCompatActivity()
{

        override fun onCreate(savedInstanceState: Bundle?) {
            super.onCreate(savedInstanceState)
            setContentView(R.layout.activity_landing)

            // Property 1
            val property1Button = findViewById<ImageButton>(R.id.imageButton7)
            property1Button.setOnClickListener {
                openPropertyDetails("Property 1", "Description for Property 1", R.drawable.images)
            }

            // Property 2
            val property2Button = findViewById<ImageButton>(R.id.imageButton8)
            property2Button.setOnClickListener {
                openPropertyDetails("Property 2", "Description for Property 2", R.drawable._60x470xc)
            }

            // Property 3
            val property3Button = findViewById<ImageButton>(R.id.imageButton9)
            property3Button.setOnClickListener {
                openPropertyDetails("Property 3", "Description for Property 3", R.drawable._60x470xc__1_)
            }

            // Property 4
            val property4Button = findViewById<ImageButton>(R.id.imageButton10)
            property4Button.setOnClickListener {
                openPropertyDetails("Property 4", "Description for Property 4", R.drawable.__bedroom_apartment_for_sale_ewan_residence_1_lp46040_200f39ee9fdb2600)
            }

            // Optional: Sign up button functionality
            val signUpButton = findViewById<Button>(R.id.button6)
            signUpButton.setOnClickListener {
                // Example: go to a SignUp page
                startActivity(Intent(this, ApplicationActivity::class.java))
            }
        }

        private fun openPropertyDetails(title: String, description: String, imageResId: Int) {
            val intent = Intent(this, PropertyInfoActivity::class.java)
            intent.putExtra("PROPERTY_TITLE", title)
            intent.putExtra("PROPERTY_DESCRIPTION", description)
            intent.putExtra("PROPERTY_IMAGE", imageResId)
            startActivity(intent)
        }
    }
