package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import androidx.appcompat.app.AppCompatActivity

class LandingActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_landing)

        // "BROWSE" -> open a featured property in PropertyInfoActivity
        findViewById<Button>(R.id.button8).setOnClickListener {
            openPropertyDetails(
                title = "Property 1",
                description = "Description for Property 1",
                imageResId = R.drawable.images
            )
        }

        // Property 1
        findViewById<ImageButton>(R.id.imageButton7).setOnClickListener {
            openPropertyDetails("Property 1", "Description for Property 1", R.drawable.images)
        }

        // Property 2
        findViewById<ImageButton>(R.id.imageButton8).setOnClickListener {
            openPropertyDetails("Property 2", "Description for Property 2", R.drawable._60x470xc)
        }

        // Property 3
        findViewById<ImageButton>(R.id.imageButton9).setOnClickListener {
            openPropertyDetails("Property 3", "Description for Property 3", R.drawable._60x470xc__1_)
        }

        // Property 4
        findViewById<ImageButton>(R.id.imageButton10).setOnClickListener {
            openPropertyDetails(
                "Property 4",
                "Description for Property 4",
                R.drawable.__bedroom_apartment_for_sale_ewan_residence_1_lp46040_200f39ee9fdb2600
            )
        }

        // Sign up button -> Application form
        findViewById<Button>(R.id.button6).setOnClickListener {
            startActivity(Intent(this, ApplicationActivity::class.java))
        }
    }

    private fun openPropertyDetails(title: String, description: String, imageResId: Int) {
        val intent = Intent(this, PropertyInfoActivity::class.java).apply {
            putExtra("PROPERTY_TITLE", title)
            putExtra("PROPERTY_DESCRIPTION", description)
            putExtra("PROPERTY_IMAGE", imageResId)
        }
        startActivity(intent)
    }
}

